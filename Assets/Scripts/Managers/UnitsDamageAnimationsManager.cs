// Roman Baranov 24.05.2022

using Spine.Unity;
using System.Collections;
using UnityEngine;

public class UnitsDamageAnimationsManager : MonoBehaviour
{
    #region VARIABLES
    private IEnumerator _animationCoroutine = null;

    [SerializeField] private Transform _leftAnimationPoint = null;
    [SerializeField] private Transform _RightAnimationPoint = null;

    private SkeletonAnimation _attacker = null;

    private MeshRenderer _attackerMeshRenderer = null;
    private int _atackerStartOrder;

    private Vector2 _attackerStartPos = Vector2.zero;
    private Vector2 _attackerEndPos = Vector2.zero;

    private Vector2 _attackerStartScale = Vector2.zero;
    private Vector2 _attackerEndScale = new Vector2(1.5f, 1.5f);


    private SkeletonAnimation _target = null;

    MeshRenderer _targetMeshRenderer = null;
    private int _targetStartOrder;

    private Vector2 _targetStartPos = Vector2.zero;
    private Vector2 _targetEndPos = Vector2.zero;

    private Vector2 _targetStartScale = Vector2.zero;
    private Vector2 _targetEndScale = new Vector2(1.5f, 1.5f);

    private float _lerpMoveTime = 1.5f;

    #endregion
    private void Awake()
    {
        GameplayEvents.OnDamageAnimationStart.AddListener(StartAnimations);
    }

    #region CALLBACK Handlers
    /// <summary>
    /// Starts animations coroutine
    /// </summary>
    private void StartAnimations()
    {
        _attacker = BattleManager.Instance.AttackerUnit.GetComponent<SkeletonAnimation>();
        _target = BattleManager.Instance.TargetUnit.GetComponent<SkeletonAnimation>();

        if (!_attacker)
        {
            Debug.LogError("Attacker in not set");
        }

        if (!_target)
        {
            Debug.LogError("Target in not set");
        }

        // Init start values for attacker
        _attackerStartPos = _attacker.transform.position;
        _attackerStartScale = _attacker.transform.localScale;
        _attackerMeshRenderer = _attacker.GetComponent<MeshRenderer>();
        _atackerStartOrder = _attackerMeshRenderer.sortingOrder;

        // Init start values for target
        _targetStartPos = _target.transform.position;
        _targetStartScale = _target.transform.localScale;
        _targetMeshRenderer = _target.GetComponent<MeshRenderer>();
        _targetStartOrder = _targetMeshRenderer.sortingOrder;

        // Detect atacker end point
        if (_attacker.gameObject.GetComponent<Unit>().Side == UnitSide.LeftSide)
        {
            _attackerEndPos = _leftAnimationPoint.position;
        }
        else
        {
            _attackerEndPos = _RightAnimationPoint.position;
        }

        // Detect target end point
        if (_target.gameObject.GetComponent<Unit>().Side == UnitSide.LeftSide)
        {
            _targetEndPos = _leftAnimationPoint.position;
        }
        else
        {
            _targetEndPos = _RightAnimationPoint.position;
        }

        // Check if another animation coroutine is running
        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;
        }

        // Start animation coroutine
        _animationCoroutine = DamageAnimations();
        StartCoroutine(_animationCoroutine);
    }
    #endregion

    #region COROUTINES
    /// <summary>
    /// Starts sequence of units damage animations
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageAnimations()
    {
        // Set new sorting order values to get units above the rest
        _attackerMeshRenderer.sortingOrder = 1;
        _targetMeshRenderer.sortingOrder = 1;

        float lerp = 0f;
        while (lerp < 0.9f)
        {
            lerp += _lerpMoveTime * Time.deltaTime;
            // Scale and move units to corresponding positions
            MoveLerp(_attacker.transform, _attackerStartPos, _attackerEndPos, lerp);
            ScaleLerp(_attacker.transform, _attackerStartScale, _attackerEndScale, lerp);

            MoveLerp(_target.transform, _targetStartPos, _targetEndPos, lerp);
            ScaleLerp(_target.transform, _targetStartScale, _targetEndScale, lerp);

            yield return null;
        }

        // Play attack animations
        _attacker.loop = false;
        _attacker.AnimationName = "Miner_1";

        yield return new WaitForSeconds(.5f); 

        _target.loop = false;
        _target.AnimationName = "Damage";

        yield return new WaitForSeconds(.5f);

        // Move units back to their positions
        lerp = 0f;
        while (lerp < 0.9f)
        {
            lerp += _lerpMoveTime * Time.deltaTime;
            // Scale and move units to corresponding positions
            MoveLerp(_attacker.transform, _attackerEndPos, _attackerStartPos, lerp);
            ScaleLerp(_attacker.transform, _attackerEndScale, _attackerStartScale, lerp);

            MoveLerp(_target.transform, _targetEndPos, _targetStartPos, lerp);
            ScaleLerp(_target.transform, _targetEndScale, _targetStartScale, lerp);

            yield return null;
        }

        // Set initial order in layer values
        _attackerMeshRenderer.sortingOrder = _atackerStartOrder;
        _targetMeshRenderer.sortingOrder = _targetStartOrder;

        // Set looped Idle anaimations
        _attacker.loop = false;
        _attacker.AnimationName = "Idle";

        _target.loop = true;
        _target.AnimationName = "Idle";

        // Change game state to ChooseSide_State
        GameStateMachine.Instance.StateMachine.ChangeState(GameStateId.ChooseSideState);
        yield return null;
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Linearly moves unit from point A to Point B
    /// </summary>
    /// <param name="unit">Unit to move</param>
    /// <param name="pointA">Start point</param>
    /// <param name="pointB">End point</param>
    /// <param name="t">Lerp speed</param>
    private void MoveLerp(Transform unit, Vector2 pointA, Vector2 pointB, float t)
    {
        unit.position = Vector2.Lerp(pointA, pointB, t);
    }

    /// <summary>
    /// Linearly scales unit 
    /// </summary>
    /// <param name="unit">Unit to scale</param>
    /// <param name="startScale">Start scale values</param>
    /// <param name="endScale">End scale values</param>
    /// <param name="t">Lerp speed</param>
    private void ScaleLerp(Transform unit, Vector2 startScale, Vector2 endScale, float t)
    {
        unit.localScale = Vector2.Lerp(startScale, endScale, t);
    }
    #endregion
}

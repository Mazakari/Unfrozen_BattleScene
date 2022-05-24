//Roman Baranov 24.05.2022

using UnityEngine;

/// <summary>
/// Singletone that controls battle flow
/// </summary>
public class BattleManager : MonoBehaviour
{
    #region VARIABLES
    public static BattleManager Instance = null;

    /// <summary>
    /// Currently attacking side.
    /// </summary>
    public UnitSide AttackSide { get; set; }

    /// <summary>
    /// Attacking unit
    /// </summary>
    public Unit AttackerUnit { get; set; } = null;

    /// <summary>
    /// Unit that being attacked
    /// </summary>
    public Unit TargetUnit { get; set; } = null;

    /// <summary>
    /// Is player pressed attack button to attack target
    /// </summary>
    public bool IsAttackButtonPressed { get; set; } = false;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        if (Instance)
        {
            Instance = null;
        }

        Instance = this;

        // Handle player attack button press callback
        GameplayEvents.OnAttckButtonPressed.AddListener(AttackButtonPressed);

        // Handle player skip button press callback
        GameplayEvents.OnSkipTurnButtonPressed.AddListener(SkipTurn);

        // Handle target unit selection callback
        GameplayEvents.OnTargetUnitSelected.AddListener(SetTargetUnit);

        // Set reference to random attacker side unit
        GameplayEvents.OnSideTurn.AddListener(SetAttackerUnit);
    }
    #endregion

    #region CALLBACKS Handler
    /// <summary>
    /// Set attack button flag to true
    /// </summary>
    private void AttackButtonPressed()
    {
        IsAttackButtonPressed = true;
    }

    /// <summary>
    /// Skip current turn and set state to chose side
    /// </summary>
    private void SkipTurn()
    {
        AttackerUnit = null;
        TargetUnit = null;

        IsAttackButtonPressed = false;

        GameStateMachine.Instance.StateMachine.ChangeState(GameStateId.ChooseSideState);
    }

    /// <summary>
    /// Set referrence to attacker unit
    /// </summary>
    /// <param name="side">Side units collection</param>
    private void SetAttackerUnit(UnitSide side)
    {
        if (side == UnitSide.LeftSide)
        {
            AttackerUnit = UnitsManager.Instance.GetRandomUnit(UnitsManager.Instance.LeftSideUnits);
        }
        else
        {
            AttackerUnit = UnitsManager.Instance.GetRandomUnit(UnitsManager.Instance.RightSideUnits);
        }
    }

    /// <summary>
    /// Set referrence to target unit
    /// </summary>
    private void SetTargetUnit(Unit target)
    {
        TargetUnit = target;
    }
    #endregion
}

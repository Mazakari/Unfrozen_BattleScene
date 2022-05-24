// Roman Baranov 23.05.2022

using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    #region VARIABLES
    public static GameStateMachine Instance = null;

    /// <summary>
    /// Initial game state
    /// </summary>
    [SerializeField] private GameStateId _initialState;

    private StateMachine _stateMachine = null;
    /// <summary>
    /// State machine referrence
    /// </summary>
    public StateMachine StateMachine { get { return _stateMachine; } }
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        if (Instance)
        {
            Instance = null;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _stateMachine = new StateMachine(this);
        _stateMachine.RegisterState(new ChooseSide_State());
        _stateMachine.RegisterState(new ChooseTarget_State());
        _stateMachine.RegisterState(new UnitsDamage_State());

        _stateMachine.ChangeState(_initialState);
    }

    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Update();
    }
    #endregion
}

// Roman Baranov 23.05.2022

public class StateMachine
{
    #region VARIABLES
    public IGameStates[] states = null;
    public GameStateMachine gameStateMachine = null;
    public GameStateId currentState;
    #endregion

    #region CONSTRUCTOR
    public StateMachine(GameStateMachine stateMachine)
    {
        this.gameStateMachine = stateMachine;
        int numStates = System.Enum.GetNames(typeof(GameStateId)).Length;

        states = new IGameStates[numStates];
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Adds a new state to game state machine
    /// </summary>
    /// <param name="state">Game state to add</param>
    public void RegisterState(IGameStates state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }

    /// <summary>
    /// Gets current GameStateId interface
    /// </summary>
    /// <param name="stateId">State Id to search for</param>
    /// <returns>Required state</returns>
    public IGameStates GetState(GameStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }

    /// <summary>
    /// Updates current game state
    /// </summary>
    public void Update()
    {
        GetState(currentState)?.Update(gameStateMachine);
    }

    /// <summary>
    /// Changes current gamse state to the new one. Calls Exit() of the current state, then Enter() of the new state 
    /// </summary>
    /// <param name="newState">New state to switch to</param>
    public void ChangeState(GameStateId newState)
    {
        GetState(currentState)?.Exit(gameStateMachine);
        currentState = newState;
        GetState(currentState)?.Enter(gameStateMachine);
    }
    #endregion
}

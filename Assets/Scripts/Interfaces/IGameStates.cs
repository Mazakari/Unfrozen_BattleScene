// Roman Baranov 23.05.2022

#region ENUM
/// <summary>
/// Available game states
/// </summary>
public enum GameStateId
{
    ChooseSideState,
    ChooseTargetState,
    UnitsDamageState
}
#endregion

#region INTERFACE
public interface IGameStates
{
    /// <summary>
    /// Get current state Id
    /// </summary>
    /// <returns>Return current state</returns>
    public GameStateId GetId();

    /// <summary>
    /// New state to enter
    /// </summary>
    /// <param name="agent"></param>
    public void Enter(GameStateMachine agent);

    /// <summary>
    /// Update current state
    /// </summary>
    /// <param name="agent"></param>
    public void Update(GameStateMachine agent);

    /// <summary>
    /// Exit current state
    /// </summary>
    /// <param name="agent"></param>
    public void Exit(GameStateMachine agent);
}
#endregion

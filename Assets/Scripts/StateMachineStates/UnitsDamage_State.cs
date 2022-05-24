// Roman Baranov 23.05.2022

using UnityEngine;

/// <summary>
/// Handles units damage animations
/// </summary>
public class UnitsDamage_State : IGameStates
{
    #region STATES
    public GameStateId GetId()
    {
        return GameStateId.UnitsDamageState;
    }

    public void Enter(GameStateMachine stateMachine)
    {
        // Disable turn controls during animations
        GameplayEvents.OnTurnButtonsAvailable.Invoke(false);

        // Call for start unit damage animations
        GameplayEvents.OnDamageAnimationStart.Invoke();
       
    }
    public void Update(GameStateMachine stateMachine)
    {
    }

    public void Exit(GameStateMachine stateMachine)
    {
        // Reset attack button state
        BattleManager.Instance.IsAttackButtonPressed = false;
    }
    #endregion

    
}

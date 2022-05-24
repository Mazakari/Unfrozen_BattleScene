// Roman Baranov 23.05.2022

using UnityEngine;
using UnityEngine.Events;

public class ChooseSide_State : IGameStates
{
    #region STATES
    public GameStateId GetId()
    {
        return GameStateId.ChooseSideState;
    }

    public void Enter(GameStateMachine stateMachine)
    {
        // Deactivate attack and skip turn buttons
        GameplayEvents.OnTurnButtonsAvailable.Invoke(false);

        // Decide which side turn to act
        int side = Random.Range(0, 11);
        if (side <= 5)
        {
            BattleManager.Instance.AttackSide = UnitSide.LeftSide;
        }
        else
        {
            BattleManager.Instance.AttackSide = UnitSide.RightSide;
        }

        GameplayEvents.OnSideTurn.Invoke(BattleManager.Instance.AttackSide);

        // Switch game state to pick target state
        stateMachine.StateMachine.ChangeState(GameStateId.ChooseTargetState);
    }

    public void Update(GameStateMachine stateMachine)
    {
    }

    public void Exit(GameStateMachine stateMachine)
    {
    }
    #endregion
}

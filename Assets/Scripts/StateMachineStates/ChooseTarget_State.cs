// Roman Baranov 23.05.2022

using UnityEngine;

/// <summary>
/// State handles target selection
/// </summary>
public class ChooseTarget_State : IGameStates
{
    #region VARIABLES
    private Vector3 _mousePos = Vector2.zero;

    #endregion

    #region STATES
    public GameStateId GetId()
    {
        return GameStateId.ChooseTargetState;
    }

    public void Enter(GameStateMachine stateMachine)
    {
        GameplayEvents.OnTurnButtonsAvailable.Invoke(true);
    }
    public void Update(GameStateMachine stateMachine)
    {
        if (BattleManager.Instance.IsAttackButtonPressed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mousePos.z = 0;

                // Check if unit clicked is from opposite defensing team
                RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector2.zero);
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit && unit.Side != BattleManager.Instance.AttackSide)
                {
                    // Send callback to set reference for target unit
                    GameplayEvents.OnTargetUnitSelected.Invoke(unit);

                    // Switch to UnitsDamage_State
                    stateMachine.StateMachine.ChangeState(GameStateId.UnitsDamageState);
                }
            }
        }
    }

    public void Exit(GameStateMachine stateMachine)
    {
    }
    #endregion

    #region CALLBACK Handlers
   
    #endregion
}

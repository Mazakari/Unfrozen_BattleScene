// Roman Baranov 23.05.2022

using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject _turnAnouncementPanel = null;
    [SerializeField] private TMP_Text _turnBunchPanel = null;
    #endregion

    #region UNITY Methods
    private void Awake()
    {
        GameplayEvents.OnSideTurn.AddListener(SideTurn);
    }
    #endregion

    #region CALLBACK Handlers
    /// <summary>
    /// Update UI side turn panel text
    /// </summary>
    /// <param name="side">New side turn name</param>
    private void SideTurn(UnitSide side)
    {
        string turnName = "";
        if (side == UnitSide.LeftSide)
        {
            turnName = "Left Side Turn";
        }
        else
        {
            turnName = "Right Side Turn";
        }

        _turnBunchPanel.text = turnName;
        _turnAnouncementPanel.SetActive(true);
    }
    #endregion
}

// Roman Baranov 24.05.2022

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls usage of turn control buttons - Attack and Skip turn
/// </summary>
public class TurnControlButtons : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Button _attackButton = null;
    [SerializeField] private Button _skipTurnButton = null;
    #endregion

    #region UNITY Methods
    // Start is called before the first frame update
    private void Awake()
    {
        GameplayEvents.OnTurnButtonsAvailable.AddListener(UpdateButtonsState);
    }

    private void Start()
    {
        _attackButton.onClick.AddListener(Attck);
        _skipTurnButton.onClick.AddListener(SkipTurn);
    }
    #endregion

    #region CALLBACK Handlers
    /// <summary>
    /// Enable or disable buttons
    /// </summary>
    /// <param name="isEnabled">New buttons state</param>
    private void UpdateButtonsState(bool isEnabled)
    {
        _attackButton.enabled = isEnabled;
        _skipTurnButton.enabled = isEnabled;
    }

    /// <summary>
    /// Handles player press on attack button
    /// </summary>
    private void Attck()
    {
        GameplayEvents.OnAttckButtonPressed.Invoke();
    }

    /// <summary>
    /// Handles player press on skip turn button
    /// </summary>
    private void SkipTurn()
    {
        GameplayEvents.OnSkipTurnButtonPressed.Invoke();
    }
    #endregion
}

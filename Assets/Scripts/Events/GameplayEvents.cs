// Roman Baranov 23.05.2022

using UnityEngine.Events;

/// <summary>
/// Class container for all gameplay Unity Events
/// </summary>
public class GameplayEvents
{
    /// <summary>
    /// Send on side turn state. Put side name on invoke
    /// </summary>
    public static readonly UnityEvent<UnitSide> OnSideTurn = new UnityEvent<UnitSide>();

    /// <summary>
    /// Send when player can use turn buttons like Attack and Skip Turn. Put current attack state on invoke
    /// </summary>
    public static readonly UnityEvent<bool> OnTurnButtonsAvailable = new UnityEvent<bool>();

    /// <summary>
    /// Send when player clicked on "Attack" button.
    /// </summary>
    public static readonly UnityEvent OnAttckButtonPressed = new UnityEvent();

    /// <summary>
    /// Send when player clicked on turn "Skip Turn" button.
    /// </summary>
    public static readonly UnityEvent OnSkipTurnButtonPressed = new UnityEvent();

    /// <summary>
    /// Send when player clicked on target unit to attck.
    /// </summary>
    public static readonly UnityEvent<Unit> OnTargetUnitSelected = new UnityEvent<Unit>();

    /// <summary>
    /// Send when need to start playing unit attack animations.
    /// </summary>
    public static readonly UnityEvent OnDamageAnimationStart = new UnityEvent();
}

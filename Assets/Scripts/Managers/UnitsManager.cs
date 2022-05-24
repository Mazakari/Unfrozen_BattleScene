// Roman Baranov 23.05.2022

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singletone that handle units operations
/// </summary>
public class UnitsManager : MonoBehaviour
{
    #region VARIABLES
    public static UnitsManager Instance = null;

    /// <summary>
    /// Collection of left side units
    /// </summary>
    [field: SerializeField] public List<Unit> LeftSideUnits { get; private set; } = null;
    /// <summary>
    /// Collection of right side units
    /// </summary>
    [field: SerializeField] public List<Unit> RightSideUnits { get; private set; } = null;
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
    void Start()
    {
        // Set corresponding side for units collections
        SetUnitSide(LeftSideUnits, UnitSide.LeftSide);
        SetUnitSide(RightSideUnits, UnitSide.RightSide);
    }
    #endregion

    #region PUBLIC Methods
    /// <summary>
    /// Get random unit from the units collection
    /// </summary>
    /// <param name="units">Units collection</param>
    /// <returns>Random unit</returns>
    public Unit GetRandomUnit(List<Unit> units)
    {
        int rnd = Random.Range(0, units.Count);
        Unit unit = units[rnd];

        return unit;
    }
    #endregion

    #region PRIVATE Methods
    /// <summary>
    /// Set side for given units collection
    /// </summary>
    /// <param name="units">Units collection to set side</param>
    /// <param name="side">Side to set for units</param>
    private void SetUnitSide(List<Unit> units, UnitSide side)
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].Side = side;
        }
    }

    #endregion
}

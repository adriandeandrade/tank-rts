using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    // Private
    [BoxGroup("Debug"), SerializeField, ReadOnly] private List<BaseUnit> selectedUnits;

    // Actions
    public System.Action<BaseUnit> OnUnitSelected;
    public System.Action<BaseUnit> OnUnitDeselected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SelectUnit(BaseUnit unit)
    {
        if (selectedUnits.Contains(unit)) return;

        selectedUnits.Add(unit);
        OnUnitSelected?.Invoke(unit);
    }

    public void DeselectUnit(BaseUnit unit)
    {
        if (selectedUnits.Contains(unit))
        {
            selectedUnits.Remove(unit);
            OnUnitDeselected?.Invoke(unit);
        }
    }
}

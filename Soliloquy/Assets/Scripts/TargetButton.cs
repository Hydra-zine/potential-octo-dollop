using System;
using TMPro;
using UnityEngine;

public class TargetButton : MonoBehaviour
{
    private Unit targetUnit;
    private Action<Unit> callback;
    [SerializeField] private TextMeshProUGUI tmp;


    public void Initialize(Unit target, Action<Unit> onChosen)
    {
        targetUnit = target;
        callback = onChosen;
        tmp.text = target.name;
    }

    public Unit getUnit()
    {
        return targetUnit;
    }

    public void OnClick()
    {
        callback?.Invoke(targetUnit);
    }
}

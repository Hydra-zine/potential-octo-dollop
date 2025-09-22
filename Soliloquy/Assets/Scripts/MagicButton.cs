using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicButton : MonoBehaviour
{
    private ActionAsset actionAsset;
    private Unit user;
    private Action<ActionAsset> callback;

    [SerializeField] private TextMeshProUGUI label;

    public void Initialize(Unit caster, ActionAsset action, Action<ActionAsset> onChosen)
    {
        user = caster;
        actionAsset = action;
        callback = onChosen;

        label.text = $"{actionAsset.actionName} ({actionAsset.MPCost} MP)";
        
    }

    public void OnClick()
    {
        callback?.Invoke(actionAsset);
    }
}

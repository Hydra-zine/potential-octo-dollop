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
    [SerializeField] private Button button;

    public void Initialize(Unit caster, ActionAsset action, Action<ActionAsset> onChosen)
    {
        user = caster;
        actionAsset = action;
        callback = onChosen;

        label.text = $"{actionAsset.actionName} ({actionAsset.MPCost} MP)";
        button.onClick.AddListener(() => callback?.Invoke(actionAsset));
    }
}

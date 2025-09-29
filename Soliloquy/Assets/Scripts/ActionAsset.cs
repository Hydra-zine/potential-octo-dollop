using System.Collections;
using UnityEngine;
public enum ActionType
{
    Damage,
    Heal,
    Buff,
    Debuff,
    Status,
    Utility
}

public abstract class ActionAsset : ScriptableObject
{

    public string actionName;
    public int MPCost;

    public ActionType actionType;
    public abstract IEnumerator Execute(Unit user, Unit target);
    
}

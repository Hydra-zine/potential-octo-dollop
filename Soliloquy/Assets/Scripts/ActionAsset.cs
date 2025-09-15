using System.Collections;
using UnityEngine;

public abstract class ActionAsset : ScriptableObject
{

    public string actionName;
    public int MPCost;
    public abstract IEnumerator Execute(Unit user, Unit target);
    
}

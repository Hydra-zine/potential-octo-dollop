using UnityEngine;

[CreateAssetMenu(menuName = "Battle/SharedVariables")]
public class SharedVariables : ScriptableObject
{
    public int maxMP = 100;
    public int currentMP;
}
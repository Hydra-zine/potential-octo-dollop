using UnityEngine;


[CreateAssetMenu(fileName = "NewNPCData", menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    
    [TextArea(3, 10)]
    public string[] dialogue;
}

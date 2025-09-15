using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "Actions/Attack")]
public class AttackAction : ActionAsset
{
    public int damage;
    public override IEnumerator Execute(Unit user, Unit target)
    {
        target.HP -= damage * user.ATK / user.DEF + 1;
        Debug.Log($"{user.name} lowkey hit {target.name} for {damage} owchie points!");
        yield return new WaitForSeconds(1f);
    }
}

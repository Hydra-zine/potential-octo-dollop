using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAction", menuName = "Actions/Attack")]
public class AttackAction : ActionAsset
{
    public int power;
    public override IEnumerator Execute(Unit user, Unit target)
    {
        int damage = power * user.ATK / user.DEF + 1;

        Debug.Log($"{user.name} lowkey hit {target.name} for {damage} owchie points!");
        target.TakeDamage(damage);
        yield return user.FinishTurn();
    }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Magic/Fireball")]
public class Fireball : ActionAsset
{
    public int power = 20;

    public override IEnumerator Execute(Unit user, Unit target)
    {
        if (user.MGA < MPCost)
        {
            Debug.Log($"{user.UnitName} tried to cast {actionName} but didn’t have enough MP!");
            yield break;
        }

        user.MGA -= MPCost;
        int damage = Mathf.RoundToInt((float)user.MGA / target.MGD * power);
        target.TakeDamage(damage);

        Debug.Log($"{user.UnitName} cast {actionName} on {target.UnitName} for {damage} damage!");

        //yield return user.FinishTurn();
    }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Magic/MagicAttack")]
public class MagicAttack : ActionAsset
{
    public int power = 20;
    [SerializeField] SharedMP sharedMP;

    public override IEnumerator Execute(Unit user, Unit target)
    {
        if (sharedMP.currentMP < MPCost)
        {
            Debug.Log($"{user.UnitName} tried to cast {actionName} but didn’t have enough MP!");
            yield break;
        }

        sharedMP.Spend(MPCost);
        int damage = Mathf.RoundToInt((float)user.MGA / target.MGD * power);

        Debug.Log($"{user.UnitName} cast {actionName} on {target.UnitName} for {damage} damage!");
        
        target.TakeDamage(damage);
        yield return user.FinishTurn();
    }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Actions/Regen")]
public class RegenAction : ActionAsset
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SharedMP sharedMP;

    public override IEnumerator Execute(Unit user, Unit target)
    {
        int regenAmount = (user.MGA + user.MGD) / 2;
        sharedMP.Restore(regenAmount);

        yield return user.FinishTurn();
    }
}

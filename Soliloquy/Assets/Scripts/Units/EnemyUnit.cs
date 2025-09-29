using System.Collections;
using UnityEngine;

public class EnemyUnit : Unit
{
    public override void TakeTurn()
    {
        // Start enemy decision-making
        StartCoroutine(EnemyAction());
    }

    private IEnumerator EnemyAction()
    {
        yield return new WaitForSeconds(1f);

        Unit target = EnemyAI.ChooseTarget(this, BattleSystem.Instance.playerUnits);
        ActionAsset action = EnemyAI.ChooseAction(this, target);

        yield return action.Execute(this, target);

    }
}
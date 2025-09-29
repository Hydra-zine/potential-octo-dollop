using UnityEngine;

public class Berserker : Unit
{
    
    private int turnsRaged = 0;

    public override void TakeTurn()
    {
        if (HP < MAXHP * 0.3f) PerformAttack(BattleSystem.Instance.enemyUnits[0]);
        BattleHUD.Instance.ShowActions(this);
    }

}
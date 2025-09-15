using UnityEngine;

public class Mage : Unit
{
    public override void TakeTurn()
    {
        ShuffleSpells();
        BattleHUD.Instance.ShowActions(this);
    }

    private void ShuffleSpells()
    {
        
    }
    
}
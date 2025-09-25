using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
{

    [SerializeField] private int spellSlots = 3;
    // public int SpellSlots => spellSlots;
    private List<ActionAsset> shuffledSpells = new List<ActionAsset>();
    public override void TakeTurn()
    {

        ShuffleSpells();
        BattleHUD.Instance.ShowActions(this);
    }

    private void ShuffleSpells()
    {
        shuffledSpells.Clear();
        List<ActionAsset> temp = new List<ActionAsset>(attacks);

        for (int i = 0; i < spellSlots && temp.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, temp.Count);
            shuffledSpells.Add(temp[randomIndex]);
            temp.RemoveAt(randomIndex);
        }
    }

    public List<ActionAsset> GetShuffledSpells()
    {
        return shuffledSpells;
    }
    public void IncreaseSpellSlots(int amt)
    {
        spellSlots += amt;
    }
    
}
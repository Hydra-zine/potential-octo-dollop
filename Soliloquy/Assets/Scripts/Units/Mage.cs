using System.Collections.Generic;
using UnityEngine;

public class Mage : Unit
{

    public override bool IsControllable { get; set; } = false;
    public override void TakeTurn()
    {

        ShuffleSpells();
    }

    private void ShuffleSpells()
    {
        
    }
    
}
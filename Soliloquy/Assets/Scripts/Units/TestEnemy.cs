using System.Collections;
using UnityEngine;

public class TestEnemy : Unit
{
    public override void TakeTurn()
    {
        Debug.Log($"{UnitName} does abslutely nothing!");
        StartCoroutine(FinishTurn());
    }

}

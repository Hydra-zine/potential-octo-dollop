using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private int turnIndex;
    private List<Unit> allUnits = new List<Unit>();



    public void InitializeBattle(List<Unit> units)
    {
        allUnits = units.OrderByDescending(u => u.SPD).ToList();
        turnIndex = 0;
    }

    public void nextTurn()
    {
        if (allUnits.Count == 0) return;
        Unit current = allUnits[turnIndex % allUnits.Count];



        if (!current.IsAlive)
        {
            advanceTurn();
            return;
        }

        Debug.Log($"its {current.name}'s turn");
        current.TakeTurn();

    }

    public void advanceTurn()
    {
        turnIndex++;
        nextTurn();
    }



}

using System.Collections.Generic;
using UnityEngine;


public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [SerializeField] GameObject[] PlayerPrefabs;
    [SerializeField] Transform[] PlayerPositions;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform[] enemyPositions;

    private List<Unit> units = new List<Unit>();
    [HideInInspector] public List<Unit> playerUnits = new List<Unit>();
    [HideInInspector] public List<Unit> enemyUnits = new List<Unit>();


    void Start()
    {
        state = BattleState.START;

        for (int i = 0; i < PlayerPrefabs.Length; i++)
        {
            if (i == 4) break;
            GameObject playerGO = Instantiate(PlayerPrefabs[i], PlayerPositions[i]);
            Unit unit = playerGO.GetComponent<Unit>();
            units.Add(unit);
            playerUnits.Add(unit);
            unit.OnDeath += HandleUnitDeath;
        }

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            GameObject enemyGO = Instantiate(enemyPrefabs[i], enemyPositions[i]);
            Unit unit = enemyGO.GetComponent<Unit>();
            units.Add(unit);
            enemyUnits.Add(unit);
            unit.OnDeath += HandleUnitDeath;
        }

        TurnManager.Instance.InitializeBattle(units);
        TurnManager.Instance.nextTurn();

    }

    private void HandleUnitDeath(Unit deadUnit)
    {
        Debug.Log($"BattleSystem: {deadUnit.UnitName} removed from battle.");

        units.Remove(deadUnit);
        playerUnits.Remove(deadUnit);
        enemyUnits.Remove(deadUnit);

        BattleHUD.Instance.RemoveUnit(deadUnit);

        Destroy(deadUnit.gameObject);

        if (enemyUnits.Count == 0)
            Debug.Log("yoooooo");
        else if (playerUnits.Count == 0)
            Debug.Log("bruhhhh");
    }


}
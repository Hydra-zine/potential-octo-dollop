using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] GameObject ActionPanel;
    [SerializeField] GameObject magicPanel;
    [SerializeField] BattleSystem bs;

    private Unit currentUnit;
    //public HPBar hpBar;

    [SerializeField] private GameObject targetButtonPrefab;
    [SerializeField] private GameObject enemyTargetPanel;
    [SerializeField] private GameObject playerTargetPanel;

    private List<TargetButton> activeEnemyButtons = new List<TargetButton>();
    private List<TargetButton> activePlayerButtons = new List<TargetButton>();

    public static BattleHUD Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    // Clear old buttons

    private void ShowTargetButtons(List<Unit> targets, GameObject panel, List<TargetButton> buttonList, Action<Unit> OnChosen)
    {
        ClearTargetButtons(buttonList);
        panel.SetActive(true);


        foreach (Unit unit in targets)
        {
            GameObject buttonGO = Instantiate(targetButtonPrefab, panel.transform);
            TargetButton tb = buttonGO.GetComponent<TargetButton>();

            tb.Initialize(unit, chosenUnit =>
            {
                OnChosen(chosenUnit);
                ClearTargetButtons(buttonList);
                StartCoroutine(currentUnit.FinishTurn());
            });

            buttonList.Add(tb);
        }


    }
    private void ClearTargetButtons(List<TargetButton> buttonList)
    {
        enemyTargetPanel.SetActive(false);
        foreach (TargetButton tb in buttonList)
        {
            Destroy(tb.gameObject);
        }
        buttonList.Clear();
    }


    public void ShowActions(Unit unit)
    {
        currentUnit = unit;
        ActionPanel.SetActive(true);
        magicPanel.SetActive(false);
    }
    public void OnAttackButton()
    {
        ActionPanel.SetActive(false);
        ShowTargetButtons(bs.enemyUnits, enemyTargetPanel, activeEnemyButtons, currentUnit.PerformAttack);
    }

    public void RemoveUnit(Unit deadUnit)
    {
        for (int i = activeEnemyButtons.Count - 1; i >= 0; i--)
        {
            if (deadUnit == activeEnemyButtons[i].getUnit())
            {
                Destroy(activeEnemyButtons[i].gameObject);
                activeEnemyButtons.RemoveAt(i);
            }
        }

        for (int i = activePlayerButtons.Count - 1; i >= 0; i--)
        {
            if (deadUnit == activePlayerButtons[i].getUnit())
            {
                Destroy(activePlayerButtons[i].gameObject);
                activePlayerButtons.RemoveAt(i);
            }
        }
    }

}

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] GameObject ActionPanel;
    [SerializeField] GameObject magicPanel;
    [SerializeField] BattleSystem bs;

    private Unit currentUnit;
    //public HPBar hpBar;

    [SerializeField] private GameObject targetButtonPrefab;
    [SerializeField] private GameObject magicButtonPrefab;
    [SerializeField] private GameObject enemyTargetPanel;
    [SerializeField] private GameObject playerTargetPanel;

    private List<TargetButton> activeEnemyButtons = new List<TargetButton>();
    private List<MagicButton> activeMagicButtons = new List<MagicButton>();
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
        foreach(TargetButton tb in buttonList)
        {
            Destroy(tb.gameObject);
        }
        buttonList.Clear();
    }

    private void ShowMagicButtons()
    {

        foreach (ActionAsset atk in currentUnit.attacks)
        {
            GameObject buttonGO = Instantiate(magicButtonPrefab, magicPanel.transform);
            MagicButton mb = buttonGO.GetComponent<MagicButton>();

            mb.Initialize(currentUnit, atk, chosenAttack =>
            {
                ClearMagicButtons();
                ShowTargetButtons(bs.enemyUnits, enemyTargetPanel, activeEnemyButtons, target =>
                {
                    StartCoroutine(chosenAttack.Execute(currentUnit, target));
                });
            });

        }

    }

    private void ClearMagicButtons()
    {
        magicPanel.SetActive(false);
        foreach (MagicButton mb in activeMagicButtons)
        {
            Destroy(mb.gameObject);
        }
        activeMagicButtons.Clear();
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
        magicPanel.SetActive(false);
        //Debug.Log($"bs: {bs}, enemyTargetPanel: {enemyTargetPanel}, currentUnit: {currentUnit.name}");

        ShowTargetButtons(bs.enemyUnits, enemyTargetPanel, activeEnemyButtons, currentUnit.PerformAttack);
    }

    public void OnMagicButton()
    {
        ClearMagicButtons();

        magicPanel.SetActive(true);
        ShowMagicButtons();
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

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] GameObject ActionPanel;
    [SerializeField] GameObject magicPanel;

    private Unit currentUnit;

    [SerializeField] private GameObject targetButtonPrefab;
    [SerializeField] private GameObject magicButtonPrefab;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject enemyBarPrefab;
    [SerializeField] private GameObject enemyTargetPanel;
    [SerializeField] private GameObject playerTargetPanel;
    [SerializeField] private GameObject playerHealthPanel;
    [SerializeField] private GameObject enemyHealthPanel;

    [SerializeField] private GameObject namePanel;
    [SerializeField] private TextMeshProUGUI CurrentName;

    [SerializeField] private RegenAction regenAction;

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
                // StartCoroutine(currentUnit.FinishTurn());
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

        List<ActionAsset> availableAttacks;

        if (currentUnit is Mage mage)
        {
            availableAttacks = mage.GetShuffledSpells();
        }
        else
        {
            availableAttacks = currentUnit.attacks;
        }

        foreach (ActionAsset atk in availableAttacks)
        {
            GameObject buttonGO = Instantiate(magicButtonPrefab, magicPanel.transform);
            MagicButton mb = buttonGO.GetComponent<MagicButton>();
            activeMagicButtons.Add(mb);

            mb.Initialize(currentUnit, atk, chosenAttack =>
            {
                ClearMagicButtons();
                ShowTargetButtons(BattleSystem.Instance.enemyUnits, enemyTargetPanel, activeEnemyButtons, target =>
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

    public void CreateHealthBars(List<Unit> players, List<Unit> enemies)
    {
        foreach (Unit unit in players)
        {
            GameObject hbGO = Instantiate(healthBarPrefab, playerHealthPanel.transform);
            HPBar hb = hbGO.GetComponent<HPBar>();
            unit.SetHPBar(hb);
            hb.UpdateHP(unit.HP, unit.MAXHP);
        }
        foreach (Unit unit in enemies)
        {
            GameObject hbGO = Instantiate(enemyBarPrefab, enemyHealthPanel.transform);
            HPBar hb = hbGO.GetComponent<HPBar>();
            unit.SetHPBar(hb);
            hb.UpdateHP(unit.HP, unit.MAXHP);
        }
    }


    public void ShowActions(Unit unit)
    {
        currentUnit = unit;
        namePanel.SetActive(true);
        CurrentName.text = currentUnit.UnitName;
        ActionPanel.SetActive(true);
        magicPanel.SetActive(false);
    }
    public void OnAttackButton()
    {
        clearPanels();
        //Debug.Log($"bs: {bs}, enemyTargetPanel: {enemyTargetPanel}, currentUnit: {currentUnit.name}");

        ShowTargetButtons(BattleSystem.Instance.enemyUnits, enemyTargetPanel, activeEnemyButtons, target =>
        {
            // Perform attack
            currentUnit.PerformAttack(target);

            // Only NOW finish the turn
            // StartCoroutine(currentUnit.FinishTurn());
        });
    }

    public void OnMagicButton()
    {
        ClearMagicButtons();
        magicPanel.SetActive(true);
        ShowMagicButtons();
    }

    public void OnRegenButton()
    {
        clearPanels();

        StartCoroutine(regenAction.Execute(currentUnit, null));
    }

    private void clearPanels()
    {
        ActionPanel.SetActive(false);
        magicPanel.SetActive(false);
        namePanel.SetActive(false);
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

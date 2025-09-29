using System.Collections.Generic;
using UnityEngine;

public static class EnemyAI
{
    public static Unit ChooseTarget(Unit enemy, List<Unit> playerUnits)
    {
        List<(Unit target, int weight)> choices = new List<(Unit, int)>();

        foreach (Unit unit in playerUnits)
        {
            if (!unit.IsAlive) continue;

            int weight = 10;

            if (unit.HP < 0.3f * unit.MAXHP) weight += 20;
            if (unit.DEF < enemy.ATK) weight += Random.Range(0, enemy.ATK - unit.DEF);
            else if (unit.MGD < enemy.MGA) weight += Random.Range(0, enemy.MGD - unit.MGA);

            weight += Random.Range(0, 10);

            choices.Add((unit, weight));
        }

        return WeightedPick(choices);
    }

    public static ActionAsset ChooseAction(Unit enemy, Unit target) //, EnvironmentData environment)
    {
        List<(ActionAsset action, int weight)> choices = new List<(ActionAsset, int)>();

        foreach (ActionAsset move in enemy.attacks)
        {
            int weight = 10;

            if (target.HP < 0.2f * target.MAXHP && move.actionType == ActionType.Damage) weight += 40;
            if (move.actionType == ActionType.Status && target.role == UnitRole.Healer) weight += 30;
            if (move.actionType == ActionType.Buff || move.actionType == ActionType.Debuff) weight += 25;
            if (enemy.HP < enemy.MAXHP / 3 && move.actionType == ActionType.Heal) weight += 50;
            //if (move.element == environment.currentBoost) weight += 20;

            weight += Random.Range(0, 15);

            choices.Add((move, weight));

        }

        return WeightedPick(choices);
    }


    private static T WeightedPick<T>(List<(T item, int weight)> choices)
    {
        int total = 0;
        foreach (var c in choices) total += c.weight;

        int roll = Random.Range(0, total);
        foreach (var c in choices)
        {
            if (roll < c.weight) return c.item;
            roll -= c.weight;
        }

        return choices[0].item;
    }
}

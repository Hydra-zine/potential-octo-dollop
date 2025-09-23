using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Battle/Shared MP Pool")]
public class SharedMP : ScriptableObject
{
    public int maxMP = 100;
    public int currentMP = 100;

    public event Action<int, int> OnMPChanged; // current, max

    public bool Spend(int amount)
    {
        if (currentMP < amount) return false;
        currentMP -= amount;
        OnMPChanged?.Invoke(currentMP, maxMP);
        return true;
    }

    public void Restore(int amount)
    {
        currentMP = Mathf.Min(currentMP + amount, maxMP);
        OnMPChanged?.Invoke(currentMP, maxMP);
    }

    public void Reset()
    {
        currentMP = maxMP;
        OnMPChanged?.Invoke(currentMP, maxMP);
    }
}

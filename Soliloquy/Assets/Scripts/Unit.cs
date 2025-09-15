using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

    public string UnitName;
    public int HP, SPD, ATK, DEF, MGA, MGD, LCK;
    public event System.Action<Unit> OnDeath;

    public bool IsAlive => HP > 0;

    public virtual void PerformAttack(Unit target)
    {
        int damage = ATK / target.DEF * 10;
        target.TakeDamage(damage);
        Debug.Log($"bro just hit {target.UnitName} for {damage} ouchie points no way");

    }
    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Debug.Log($"no way {UnitName} just died lmao");
            OnDeath?.Invoke(this);
        }
    }

    public virtual IEnumerator FinishTurn()
    {
        yield return new WaitForSeconds(1f);
        TurnManager.Instance.advanceTurn();
    }
    public abstract void TakeTurn();
}

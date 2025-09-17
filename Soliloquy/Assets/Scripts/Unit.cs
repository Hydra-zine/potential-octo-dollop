using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

    public string UnitName;
    public int HP, SPD, ATK, DEF, MGA, MGD, LCK;
    public event System.Action<Unit> OnDeath;
    public List<ActionAsset> attacks;

    public bool IsAlive => HP > 0;


    public virtual bool IsControllable { get; set; } = false;

    public virtual void PerformAttack(Unit target)
    {
        int damage = Mathf.RoundToInt((float)ATK / target.DEF * 10f);
        target.TakeDamage(damage);
        Debug.Log($"bro just hit {target.UnitName} for {damage} ouchie points no way");

    }
    public virtual void PerformMagicAttack(ActionAsset attack, Unit target)
    {
        int damage = Mathf.RoundToInt((float)MGA / target.MGD * 10f);
        target.TakeDamage(damage);
        Debug.Log($"bro just used {attack.name} on {target.name} for {damage} ouchie points no way");
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

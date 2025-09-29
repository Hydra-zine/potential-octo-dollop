using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitRole
{
    Mage,
    Swordsman,
    Healer,
    Berserker,
    Mechanic,
    BeastTamer,
    Mystic,
    Enemy
}

public abstract class Unit : MonoBehaviour
{

    public string UnitName;
    public int MAXHP, HP, SPD, ATK, DEF, MGA, MGD, LCK;

    public UnitRole role;

    private HPBar hpbar;
    public event System.Action<Unit> OnDeath;
    public List<ActionAsset> attacks;

    public bool IsAlive => HP > 0;


    public virtual void PerformAttack(Unit target)
    {
        int damage = Mathf.RoundToInt((float)ATK / target.DEF * 10f);
        target.TakeDamage(damage);
        Debug.Log($"bro just hit {target.UnitName} for {damage} ouchie points no way");
        StartCoroutine(FinishTurn());
    }
    
    public virtual void PerformMagicAttack(ActionAsset attack, Unit target)
    {
        int damage = Mathf.RoundToInt((float)MGA / target.MGD * 10f);
        target.TakeDamage(damage);
        Debug.Log($"bro just used {attack.actionName} on {target.UnitName} for {damage} ouchie points no way");
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
        hpbar.UpdateHP(HP, MAXHP);
    }

    public void SetHPBar(HPBar hb)
    {
        hpbar = hb;
        hpbar.UpdateHP(HP, MAXHP);
    }

    public virtual IEnumerator FinishTurn()
    {
        yield return new WaitForSeconds(1f);
        TurnManager.Instance.advanceTurn();
    }
    public abstract void TakeTurn();
}

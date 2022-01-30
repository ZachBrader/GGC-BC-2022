using UnityEngine;


//Interface for Attack type, implemented by each attack tuy

public interface AttackType
{

    public void Attack(GameObject attacker);
    public void DoDamage(GameObject target);
    public void ResetTimeSinceLastAttack();
}

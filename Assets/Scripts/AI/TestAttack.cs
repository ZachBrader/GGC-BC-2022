using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAttack : MonoBehaviour, AttackType
{
    public void Attack(GameObject attacker)
    {
        Debug.Log("AI " + attacker.name + " attacks!");

    }

    public void DoDamage(GameObject target)
    {
        Debug.Log("AI wants to do damage to " + target.name);
    }
}

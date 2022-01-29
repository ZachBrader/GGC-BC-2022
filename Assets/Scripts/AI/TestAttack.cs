using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAttack : MonoBehaviour, AttackType
{
    public void Attack(GameObject attacker)
    {
        Debug.Log("AI " + attacker.name + "attacks!");

    }
}

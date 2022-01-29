using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAttack : MonoBehaviour, AttackType
{
    public void Attack(WoofersAI attacker)
    {
        Debug.Log("AI " + attacker + "attacks!");

    }
}

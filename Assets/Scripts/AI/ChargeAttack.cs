using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAttack : MonoBehaviour, AttackType
{
    [Tooltip("How fast should the AI approach between attacks?")]
    [SerializeField]
    private float approachSpeed;



    public void Attack(GameObject attacker)
    {
        WoofersAI ai = attacker.GetComponent<WoofersAI>();
        NavMeshAgent agent = ai.Agent;
        GameObject player = GameObject.FindGameObjectWithTag("Player");


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAttack : MonoBehaviour, AttackType
{
    [Tooltip("How fast should the AI approach between attacks?")]
    [SerializeField]
    private float approachSpeed;

    [Tooltip("How fast should the AI move while dashing?")]
    [SerializeField]
    private float dashSpeed;

    [Tooltip("How long should the dash attack last?")]
    [SerializeField]
    private float dashTime;

    [Tooltip("How often should the AI attack?")]
    [SerializeField]
    private float attackFrequency;
    private float timeSinceLastAttack = 0;

    [Tooltip("How much damage should the attack do?")]
    [SerializeField]
    private int damage;

    public void Attack(GameObject attacker)
    {
        WoofersAI ai = attacker.GetComponent<WoofersAI>();
        NavMeshAgent agent = ai.Agent;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        timeSinceLastAttack += Time.deltaTime;


        //Dash if time period is met
        if (timeSinceLastAttack >= attackFrequency)
        {
            agent.speed = dashSpeed;
            agent.SetDestination(attacker.transform.position + transform.forward * 10);

            //If dash is long enough
            if (timeSinceLastAttack >= attackFrequency + dashTime)
            {
                //reset timeSinceLastAttack
                timeSinceLastAttack = 0;
            }
        }
        else
        {
            //movement between dashes
            agent.speed = approachSpeed;
            agent.SetDestination(player.transform.position);
        }

    }

    public void DoDamage(GameObject target)
    {
        target.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    public void ResetTimeSinceLastAttack()
    {
        timeSinceLastAttack = 0;
    }
}

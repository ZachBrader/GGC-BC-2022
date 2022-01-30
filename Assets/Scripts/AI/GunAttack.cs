using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunAttack : MonoBehaviour, AttackType
{
    [Tooltip("How fast should the AI approach between attacks?")]
    [SerializeField]
    private float approachSpeed;

    [Tooltip("How fast the AI should move while shooting")]
    [SerializeField]
    private float speedDuringAttack;

    [Tooltip("How long should pass between AI shots while attacking?")]
    [SerializeField]
    private float timeBetweenShots;
    private float timeSinceLastShot;

    [Tooltip("How long should the shooting last?")]
    [SerializeField]
    private float attackDuration;

    [Tooltip("How much time should pass between the AI attacking?")]
    [SerializeField]
    private float timeBetweenAttacks;
    private float timeSinceLastAttack = 0;

    [Tooltip("How much damage should the attack do?")]
    [SerializeField]
    private int damage = 1;

    [Tooltip("The bullet to spawn")]
    [SerializeField]
    private GameObject bullet;

    [Tooltip("The forward velocity the bullet spawns with")]
    [SerializeField]
    private float bulletVelocity;

    [Tooltip("The maximum possible deflection angle the bullets can take from fireing straight ahead. " +
        "The larger this number, the more spread present in the AI's barrage.")]
    [SerializeField]
    private float bulletSpread;



    public void Attack(GameObject attacker)
    {
        WoofersAI ai = attacker.GetComponent<WoofersAI>();
        NavMeshAgent agent = ai.Agent;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        timeSinceLastAttack += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;


        if (timeSinceLastAttack >= timeBetweenAttacks)
        {
            agent.speed = speedDuringAttack;

            //do attack
            if (timeSinceLastShot >= timeBetweenShots)
            {
                GameObject spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
                //rotate bullet by random ammount within bulletSpread
                spawnedBullet.transform.Rotate(new Vector3(0,Random.Range(bulletSpread/2, -bulletSpread/2), 0));
                spawnedBullet.transform.parent = null; //Make bullets not have a parent
                spawnedBullet.GetComponent<AIBullet>().attackDamage = damage;
                spawnedBullet.GetComponent<Rigidbody>().velocity = spawnedBullet.transform.forward * bulletVelocity;

                timeSinceLastShot = 0;
            }

            if (timeSinceLastAttack >= timeBetweenAttacks + attackDuration)
            {
                timeSinceLastAttack = 0;
            }
        }
        else
        {
            agent.speed = approachSpeed;
        }

        agent.SetDestination(player.transform.position);
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

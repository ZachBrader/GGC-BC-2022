using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public int attackDamage = 1;
    public int attackRange = 10;
    public LayerMask playerLayers;

    // Start is called before the first frame update
    void Start()
    {
        if (attackPoint == null)
        {
            attackPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAttack();
    }

    void MeleeAttack()
    {
        // Play Animation

        // Determine enemies in range
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);

        // Deal damage
        foreach (Collider player in hitPlayers)
        {
            Debug.Log("Hit " + player.name);
            // Damage Enemies
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

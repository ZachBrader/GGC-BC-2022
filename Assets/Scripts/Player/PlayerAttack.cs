using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firingPoint; 
    public GameObject bullet;
    public float bulletSpeed;

    PlayerWeapon playerWeapon; // Reference to currently equipped weapons

    public Animator animator;
    public float attackRange = .5f; // Replace with item's range
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = PlayerWeapon.instance;
        if (firingPoint == null)
        {
            firingPoint = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Player Clicks button to shoot
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Melee();
        }
    }

    void Melee()
    {
        Debug.Log("Melee");
        // Check current weapon
        Weapon melee = playerWeapon.currentWeapons[0];

        // Play Animation

        // Determine enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(firingPoint.position, melee.range, enemyLayers);

        // Deal damage
        foreach(Collider enemy in hitEnemies)
        {
            // Damage Enemies
            enemy.GetComponent<EnemyHealth>().TakeDamage(melee.DamageModifier);
        }

    }


    void Shoot()
    {
        Debug.Log("Range");
        // Check current weapon
        Weapon ranged = playerWeapon.currentWeapons[1];

        // Create bullet
        GameObject nBullet = Instantiate(bullet, firingPoint.position, transform.rotation) as GameObject;
        Rigidbody nBulletRigidbody = nBullet.GetComponent<Rigidbody>();

        // Assign damage
        nBullet.GetComponent<Bullet>().attackDamage = ranged.DamageModifier;

        // Shoot bullet
        nBulletRigidbody.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        if (firingPoint == null)
            return;
        Gizmos.DrawWireSphere(firingPoint.position, attackRange);
    }
}

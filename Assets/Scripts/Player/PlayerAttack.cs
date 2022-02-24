using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Ranged Weapon Variables
    public Transform firingPoint; // Used by weapons to determine where to attack 
    public GameObject bullet;
    public float bulletSpeed;

    PlayerWeapon playerWeapon; // Reference to currently equipped weapons

    public Animator animator;
    public float attackRange = .5f; // Replace with item's range
    public LayerMask enemyLayers;

    public AudioClip onAttackAudio;
    private AudioSource source;

    public float meleeSwingTime = 10f;
    private bool isSwingingMelee = false;
    private float meleeSwingCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = PlayerWeapon.instance;
        if (firingPoint == null)
        {
            firingPoint = transform;
        }

        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent user from attacking again while melee is occuring
        if (isSwingingMelee)
        {
            if (Time.time >= meleeSwingCounter)
            {
                isSwingingMelee = false;
            }
            else
            {
                return;
            }
        }

        // Player Clicks button to shoot
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Melee();
        }
    }

    void Melee()
    {
        // Play melee animation
        animator.Play("PlayerMelee");

        // Set counter for melee attack
        meleeSwingCounter = meleeSwingTime + Time.time;
        isSwingingMelee = true;

        
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

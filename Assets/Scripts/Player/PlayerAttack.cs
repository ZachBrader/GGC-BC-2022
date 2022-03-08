using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Controls for attacks
    public static bool canShoot = true;
    public static bool canMelee = true;
    public static bool canPlaceBomb = true;

    // Position for where attacks spawn from
    public Transform attackPoint;

    // Ranged Weapon Variables
    public GameObject bullet;
    public float bulletSpeed;

    // Melee Weapon Variables
    public float meleeSwingTime = 10f;
    private bool isSwingingMelee = false;
    private float meleeSwingCounter = 0f;

    // Reference to currently equipped weapons
    PlayerWeapon playerWeapon; 

    public Animator animator;
    public float attackRange = .5f; // Replace with item's range
    public LayerMask enemyLayers;

    public AudioClip onAttackAudio;
    private AudioSource source;

    

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon = PlayerWeapon.instance;
        if (attackPoint == null)
        {
            attackPoint = transform;
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
            Debug.Log("Shootinh");
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
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, melee.range, enemyLayers);

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
        GameObject nBullet = Instantiate(bullet, attackPoint.position, transform.rotation) as GameObject;
        Rigidbody nBulletRigidbody = nBullet.GetComponent<Rigidbody>();

        // Assign damage
        nBullet.GetComponent<Bullet>().attackDamage = ranged.DamageModifier;

        // Shoot bullet
        nBulletRigidbody.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

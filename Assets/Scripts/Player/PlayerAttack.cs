using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject bullet;
    public float bulletSpeed;

    PlayerWeapon playerWeapon;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // if (playerWeapon.currentWeapons[1])
            {
                Melee();
                //Shoot();
            }
        }
        

        // If gun,
        // Player will shoot

        // If melee,
        // Player will swing
    }

    void Melee()
    {
        // Play Animation

        // Determine enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(firingPoint.position, attackRange, enemyLayers);

        // Deal damage
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            // Damage Enemies
        }

    }


    void Shoot()
    {
        Debug.Log("Shooting");
        GameObject nBullet = Instantiate(bullet, firingPoint.position, firingPoint.rotation) as GameObject;
        Rigidbody nBulletRigidbody = nBullet.GetComponent<Rigidbody>();
        nBulletRigidbody.AddForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (firingPoint == null)
            return;
        Gizmos.DrawWireSphere(firingPoint.position, attackRange);
    }
}

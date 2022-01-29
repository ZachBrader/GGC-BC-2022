using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attackDamage = 1;
    public float timeTillDespawn = 5f;
    private float despawnTimeTracker = 0f;

    public string targetTag;

    // Start is called before the first frame update
    void Start()
    {
        despawnTimeTracker = timeTillDespawn + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= despawnTimeTracker)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == targetTag)
        {
            Debug.Log("Hit " + collision.transform.name);
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage);
        }

        Destroy(gameObject);
    }
}

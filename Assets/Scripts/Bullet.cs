using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeTillDespawn = 5f;
    private float despawnTimeTracker = 0f;

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
}

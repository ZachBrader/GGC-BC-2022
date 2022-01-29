using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // Reference to player's rigidbody'

    public float speed = 20f; // Speed of player

    private float horizontalInput; // Left Right
    private float forwardInput; // Forward Backward


    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Assign rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Obstacle")
        { 
            rb.velocity = Vector3.zero;
        }
    }
}

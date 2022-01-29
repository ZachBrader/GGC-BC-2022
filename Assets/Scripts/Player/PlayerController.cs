using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public
    public Interactable focus;
    public float speed = 20f; // Speed of player

    // Private
    private Rigidbody rb; // Reference to player's rigidbody'
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

        if (Input.GetKeyDown(KeyCode.P) && focus != null)
        {
            bool completedAction = focus.OnInteract(); 
            if (completedAction) // If action is completed successfully
            {
                Debug.Log("Player used the item");
                RemoveFocus(); // Remove used item
            }
        }
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

    public void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            Debug.Log(focus == null);
            if (focus != null)
            {
                RemoveFocus();
            }

            // Assign item as focus
            focus = newFocus;

            // Allow player to interact with it
            newFocus.canInteract = true;
        }
    }

    public void RemoveFocus()
    {
        if (focus != null)
        {
            focus.canInteract = false;
        }
        focus = null;
    }
}

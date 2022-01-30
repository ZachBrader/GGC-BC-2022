using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public
    public Interactable focus;
    public float speed = 20f; // Speed of player

    private CharacterController controller;

    // Private
    private Rigidbody rb; // Reference to player's rigidbody'
    private float horizontalInput; // Left Right
    private float forwardInput; // Forward Backward
    private Ray mousePos;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Assign rigidbody
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.P) && focus != null)
        {
            bool completedAction = focus.OnInteract(); 
            if (completedAction) // If action is completed successfully
            {
                RemoveFocus(); // Remove used item
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveVect = (Vector3.forward * forwardInput + Vector3.right * horizontalInput) * speed;
        controller.SimpleMove(moveVect);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500))
        {
            this.gameObject.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
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

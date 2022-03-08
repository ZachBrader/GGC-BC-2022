using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public static bool canDash;

    public float dashSpeed;
    public float dashTime;
    private bool isDashing = false;

    private PlayerController playerController;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (startTime + Time.time > Time.time)
        {
            playerController.Move((Vector3.forward * rb.velocity.x + Vector3.right * rb.velocity.z) * dashSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Finished dashing");
        isDashing = false;
    }
}

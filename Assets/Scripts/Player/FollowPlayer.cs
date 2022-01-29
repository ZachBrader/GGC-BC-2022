using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Target to follow

    public Vector3 offset = new Vector3(0, 3, 0); // Initial distance from the player
    public float pitch = 2f;

    public float yawSpeed = 100f; // How fast the camera rolls left or right

    public float zoomSpeed = 4f; // How fast the camera zooms
    public float minZoom = 1f; // Ensures zoom does not exceed lower limit
    public float maxZoom = 5f; // Ensures zoom does not exceed upper limit

    private float currentZoom = 2f; // Keeps track of how zoomed in player currently is
    private float currentYaw = 0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // Allows user to zoom in with mouse scrollwheel
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset * currentZoom; // Moves camera based on player
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw); // Rotates camera left and right
    }
}

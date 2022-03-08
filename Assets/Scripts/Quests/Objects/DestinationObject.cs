using UnityEngine;
using System;
using System.Collections;

public class DestinationObject : MonoBehaviour
{
    public event Action onPlayerArrival;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerReachedDestination();
        }
    }

    private void PlayerReachedDestination()
    {
        Debug.Log("Reached destinatino");
        Debug.Log(onPlayerArrival);
        onPlayerArrival?.Invoke();

        Destroy(this.gameObject);
    }
}

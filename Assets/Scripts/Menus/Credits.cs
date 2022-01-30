using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditsParent;

    public void OpenCredits()
    {
        creditsParent.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsParent.SetActive(false);
    }
}

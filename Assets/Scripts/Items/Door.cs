using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Animator anim;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();

        //audioSource.Play();

        Open();

    }

    void Open()
    {
        anim.SetBool("isOpen", true);
        promptManager?.ClosePrompt();
        playerController.RemoveFocus();

        this.enabled = false;
    }

}

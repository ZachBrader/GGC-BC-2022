using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : Health
{
    public static Action<EnemyHealth> onDeath;
    public AudioClip onDamagedSound;
    public AudioClip onDeathSound;

    private AudioSource audio;

    ItemDrop itemDrop;

    protected override void Start()
    {
        base.Start();
        itemDrop = GetComponent<ItemDrop>();
        
        audio = GetComponentInParent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnDamage()
    {
        base.OnDamage();

        //handle audio
        audio.clip = onDamagedSound;
        audio.Play();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        onDeath?.Invoke(this);

        //handle audio
        audio.clip = onDeathSound;
        audio.Play();

        itemDrop.DropItem();
        Destroy(gameObject);
    }
    
}

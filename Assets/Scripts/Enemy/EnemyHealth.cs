using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    public AudioClip onDamagedSound;
    public AudioClip onDeathSound;

    private AudioSource audio;

    ItemDrop itemDrop;

    public delegate void OnEnemyDeath();
    public OnEnemyDeath onEnemyDeath;

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
        Debug.Log("Enemy Time");
        base.OnDeath();

        onEnemyDeath?.Invoke();

        //handle audio
        audio.clip = onDeathSound;
        audio.Play();

        itemDrop.DropItem();
        Destroy(gameObject);
    }
    
}

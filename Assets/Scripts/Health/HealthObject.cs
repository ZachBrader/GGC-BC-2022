//using System;
//using UnityEngine;

//public class HealthData
//{
//    public readonly int _maxHealth;
//    public int _health;

//    public bool _canBeDamaged;
//    public bool _isDead; // Determines if character is dead

//    public HealthData(int initiaHealth)
//    {
//        _health = _maxHealth;
//    }

//    public void TakeDamage(int damage)
//    {
//        if (_canBeDamaged || _isDead) return;

//        // Player looses health
//        _health -= damage;

//        // Check if player has died
//        if (_health <= 0)
//        {
//            OnDeath();
//        }
//        else
//        {
//            OnDamage();
//        }
//    }

//    public void GainHealth(int heal)
//    {
//        // No action taken if character is dead
//        if(_isDead) return;

//        // Lock health based on MAX_HEALTH
//        if (_health + heal <= _maxHealth)
//        {
//            // Player gains health
//            _health += heal;
//        }
//        else
//        {
//            _health = _maxHealth;
//        }

//    }

//    public void OnDeath()
//    {
//        isDead = true;
//        Debug.Log("Character " + transform.name + " is dead");
//    }

//    protected virtual void OnDamage()
//    {
//        Debug.Log("Character " + transform.name + " took damage");

//        // Initialize player blink routine
//        StartCoroutine(BlinkRoutine());

//        // Reset timer
//        timeSinceDamaged = 0;
//    }
//}

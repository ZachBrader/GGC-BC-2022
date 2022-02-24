using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Quest.Goal
{
    public List<EnemyHealth> enemies;

    public override void Initialize()
    {
        base.Initialize();

        // Grab enemy healths
        foreach (EnemyHealth enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().onEnemyDeath += OnEnemyDeath;
        }
    }

    public void OnEnemyDeath()
    {
        currentAmount += 1;
        Evaluate();
    }
}

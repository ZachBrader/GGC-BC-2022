using System.Collections.Generic;

public class KillGoal : Quest.Goal
{
    public List<EnemyHealth> enemies;

    public override void Initialize()
    {
        base.Initialize();

        // Subscribe to enemy death event
        EnemyHealth.onDeath += EnemyDied;
    }

    public void EnemyDied(EnemyHealth enemy)
    {
        // Check to see if this is the desired enemy
        bool questEnemy = enemies.Remove(enemy);

        // If enemy was the target
        if (questEnemy)
        {
            // Increment values
            currentAmount += 1;

            // Check if quest is completed
            Evaluate();
        }
    }

    public override void Complete()
    {
        base.Complete();

        // Unsubscribe observer when goal is completed
        EnemyHealth.onDeath -= EnemyDied;
    }
}

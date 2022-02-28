public class EliminationGoal : Quest.Goal
{
    public override void Initialize()
    {
        base.Initialize();

        // Subscribe to enemy death event
        EnemyHealth.onDeath += EnemyDied;
    }

    public void EnemyDied(EnemyHealth enemy)
    {
        // Increment values
        currentAmount += 1;

        // Check if quest is completed
        Evaluate();
    }

    public override void Complete()
    {
        base.Complete();

        // Unsubscribe observer when goal is complete
        EnemyHealth.onDeath -= EnemyDied;
    }
}

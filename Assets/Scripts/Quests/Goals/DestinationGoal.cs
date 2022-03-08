using System.Collections.Generic;
using UnityEngine;

public class DestinationGoal : Quest.Goal
{
    public DestinationObject destination;

    public override void Initialize()
    {
        base.Initialize();

        Debug.Log("Player has arrived");
        // Subscribe to enemy death event
        destination.onPlayerArrival += PlayerArrived;
    }

    public void PlayerArrived()
    {
        Debug.Log("Player reached destination");
        // Increment values
        currentAmount += 1;

        // Check if quest is completed
        Evaluate();
    }

    public override void Complete()
    {
        base.Complete();
    }
}

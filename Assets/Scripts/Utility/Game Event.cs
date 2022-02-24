using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string eventDescription;
}

public class EnemyDefeatedEvent : GameEvent
{
    public string enemyName;

    public EnemyDefeatedEvent(string name)
    {
        enemyName = name;
    }
}
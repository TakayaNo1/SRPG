using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : PlayableEntity
{
    private EnemyStatus Status;

    void Start()
    {
        this.CurrentSqare = MapGenerator.GetSquare(20,20);
        this.Trans = GetComponent<Transform>();
        this.Status = new EnemyStatus("Boss", 10);
        this.Status.Skills.Add(new DiceSkill(GameController, new Dice(2), "さいころ×２"));

        MoveTo(this.CurrentSqare);
    }

    void Update()
    {
        
    }

    public EnemyStatus GetEnemyStatus()
    {
        return Status;
    }
    public override EntityStatus GetStatus()
    {
        return this.Status;
    }
}

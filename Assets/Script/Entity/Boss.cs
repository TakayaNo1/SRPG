using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : PlayableEntity
{
    private EnemyStatus Status;

    override protected void Start()
    {
        base.Start();
        base.SetName("Boss");

        base.CurrentSqare = MapGenerator.GetSquare(20,20);
        base.Trans = GetComponent<Transform>();
        this.Status = new EnemyStatus("Boss", 3);
        this.Status.Skills.Add(new DiceSkill(GameController, new Dice(2), "さいころ×２"));

        MoveTo(base.CurrentSqare);
    }

    override protected void Update()
    {
        base.Update();
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

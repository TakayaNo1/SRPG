using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : PlayableEntity
{
    private readonly string BOSS_NAME = "Boss";
    private readonly int BOSS_LEVEL = 5;

    private EnemyStatus Status;

    override protected void Start()
    {
        base.Start();
        base.SetNameText(BOSS_NAME);

        base.CurrentSqare = MapGenerator.GetSquare(20,20);
        base.Trans = GetComponent<Transform>();
        this.Status = new EnemyStatus(BOSS_NAME, BOSS_LEVEL);
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

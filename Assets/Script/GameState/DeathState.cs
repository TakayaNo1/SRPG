using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DeathState : IGameState
{
    public IGameState Next(GameController Controller)
    {
        BattleState.EnemyLevel = 1;
        return null;
    }
}

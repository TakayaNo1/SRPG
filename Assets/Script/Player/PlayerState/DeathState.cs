using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DeathState : IPlayerState{
    public IPlayerState Next(GameController Controller)
    {
        BattleState.EnemyLevel = 1;
        return null;
    }
}

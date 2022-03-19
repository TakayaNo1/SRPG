using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BossStartState : IGameState{
    public IGameState Next(GameController Controller)
    {
        return new SomeTextState(new string[] { "Bossのターン" }, new BossDiceMoveState(5));
    }
}

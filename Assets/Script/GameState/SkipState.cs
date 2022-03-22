using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SkipState : IGameState
{
    /**
     * 死亡したプレイヤーを回復し、次のプレイヤーへ
     */
    public IGameState Next(GameController Controller)
    {
        Parameta HP = Controller.GetCurrentPlayer().GetParameta(EntityParamsType.HP);
        HP.Value = HP.MaxValue / 2;
        return new SomeTextState("回復中...", new EndState());
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BlankState : IGameState
{
    //テスト用状態
    public IGameState Next(GameController Controller)
    {
        return this;
    }
}

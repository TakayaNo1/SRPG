using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BlankState : IGameState
{
    public IGameState Next(GameController Controller)
    {
        return this;
    }
}

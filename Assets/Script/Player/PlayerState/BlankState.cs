using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BlankState : IPlayerState{
    public IPlayerState Next(GameController Controller)
    {
        return this;
    }
}

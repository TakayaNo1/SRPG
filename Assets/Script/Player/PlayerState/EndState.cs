using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EndState : IPlayerState{
    public IPlayerState Next(GameController Controller)
    {
        return new StartState();
    }
}

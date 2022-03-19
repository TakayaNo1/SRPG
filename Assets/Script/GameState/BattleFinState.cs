using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattleFinState : IGameState{
    public IGameState Next(GameController Controller)
    {
        Controller.GetUIController().SetBattlePanelVisible(false);
        return new EndState();
    }
}

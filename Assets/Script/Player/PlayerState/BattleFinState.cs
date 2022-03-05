using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattleFinState : IPlayerState{
    public IPlayerState Next(GameController Controller)
    {
        Controller.GetUIController().SetBattlePanelVisible(false);
        return new ButtonChooseState();
    }
}

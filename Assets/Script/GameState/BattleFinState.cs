using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattleFinState : IGameState{

    private List<EnemyStatus> Enemys;

    public BattleFinState()
    {

    }
    public BattleFinState(List<EnemyStatus> Enemys)
    {
        this.Enemys = Enemys;
    }
    /**
     * バトル終了状態
     * 敵を倒した場合、アチーブメント追加
     */
    public IGameState Next(GameController Controller)
    {
        Controller.GetUIController().SetBattlePanelVisible(false);

        if(Enemys == null)
        {
            return new EndState();
        }

        Controller.GetCurrentPlayer().GetPlayerStatus().AddAchievement(AchievementType.SlimeKillCount);

        if (isBoss())
        {
            Controller.GetCurrentPlayer().GetPlayerStatus().AddAchievement(AchievementType.BossKillCount);
        }

        return new EndState();
    }

    private bool isBoss()
    {
        foreach (EnemyStatus e in Enemys)
        {
            if (e.Name == "Boss") return true;
        }
        return false;
    }
}

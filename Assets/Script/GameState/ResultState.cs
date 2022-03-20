using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ResultState : IGameState
{
    public IGameState Next(GameController Controller)
    {
        int i = 0;
        foreach(Player p in Controller.Player)
        {
            PlayerStatus status = p.GetPlayerStatus();
            int score = 0;
            score += status.GetAchievement(AchievementType.SlimeKillCount) * 3;
            score += status.GetAchievement(AchievementType.BossKillCount) * 10;
            score -= status.GetAchievement(AchievementType.DeathCount) * 2;
            PlayerPrefs.SetString("Name_" + i, p.Name);
            PlayerPrefs.SetInt("Score_" + i, score);
            i++;
        }
        PlayerPrefs.Save();

        Controller.Reset();
        return null;
    }
}

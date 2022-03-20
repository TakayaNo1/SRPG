using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DeathState : IGameState
{
    public IGameState Next(GameController Controller)
    {
        BattleState.EnemyLevel = 1;
        Controller.GetUIController().SetBattlePanelVisible(false);

        Boss Boss = Controller.Boss;
        float distance = 0f;
        Square square = null;
        while (distance <= 10)
        {
            int X = UnityEngine.Random.Range(0, 30);
            int Z = UnityEngine.Random.Range(0, 30);
            square = MapGenerator.GetSquare(X, Z);
            float min = float.MaxValue;
            foreach(Player p in Controller.Player)
            {
                float d = (Boss.transform.position - square.transform.position).sqrMagnitude;
                if (min > d)
                {
                    min = d;
                }
            }
            distance = min;
        }
        Boss.MoveTo(square);

        Controller.GetCurrentPlayer().GetPlayerStatus().AddAchievement(AchievementType.DeathCount);

        return new SomeTextState(new string[]{ "敵のレベルがリセットされた", "BOSSがテレポートした" }, new EndState());
    }
}

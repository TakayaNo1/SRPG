using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SquareEventState : IGameState
{
    public SquareEventState()
    {

    }

    /**
     * 止まったマスのイベント状態
     */
    public IGameState Next(GameController Controller)
    {
        SquareType squareType = Controller.GetCurrentPlayer().GetSquare().Type;

        if (squareType == SquareType.ItemSquare)
        {
            IItem item = new DiceItem(Controller, new Dice(2), "さいころ×２");
            Controller.GetCurrentPlayer().GetPlayerStatus().Items.Add(item);
            return new SomeTextState(new string[] { "アイテムマスに止まった。", item.GetItemName()+"を手に入れた！" }, new EndState());
        }
        else if (squareType == SquareType.MoneySquare)
        {
            return new SomeTextState(new string[] { "マネーマスに止まった。", "100円を手に入れた！" }, new EndState());
        }
        else if (squareType == SquareType.EnemySquare)
        {
            return new SomeTextState(new string[] { "エネミーマスに止まった。", "敵に出会った！" }, new BattleState());
        }
        else if (squareType == SquareType.AccidentSquare)
        {
            return new SomeTextState(new string[] { "アクシデントマスに止まった。", "しかし、何も起きなかった。(未実装)" }, new EndState());
        }
        else if (squareType == SquareType.ShopSquare)
        {
            return new SomeTextState(new string[] { "ショップマスに止まった。", "しかし、何も起きなかった。(未実装)" }, new EndState());
        }
        else if (squareType == SquareType.ExpSquare)
        {
            List<string> lines = new List<string>();
            lines.Add("経験値マスに止まった。");
            lines.Add("10EXPを得た！");
            int level = Controller.GetCurrentPlayer().GetPlayerStatus().Exp(10);
            if (level > 0)
            {
                lines.Add("レベルが" + level + "上がった。");
            }
            return new SomeTextState(lines, new EndState());
        }
        else if (squareType == SquareType.TeleportSquare)
        {
            return new SomeTextState(new string[] { "テレポートマスに止まった。" }, new TeleportState());
        }
        else if (squareType == SquareType.HealSquare)
        {
            int remainHP = Controller.GetCurrentPlayer().GetParameta(EntityParamsType.HP).AddValue(10);
            int remainMP = Controller.GetCurrentPlayer().GetParameta(EntityParamsType.MP).AddValue(10);
            return new SomeTextState(new string[] { "回復マスに止まった。\nHPとMPが10回復した。" }, new EndState());
        }
        else
        {
            return new SomeTextState(new string[] { "何も起きなかった。" }, new EndState());
        }
    }
}

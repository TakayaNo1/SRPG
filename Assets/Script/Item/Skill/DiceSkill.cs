using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSkill : DiceItem, ISkill
{

    public DiceSkill(GameController Controller, Dice Dice, string name): base(Controller, Dice, name)
    {
    }

    public new void Action()
    {
        Player P = this.Controller.GetCurrentPlayer();
        int mp = P.GetParameta(EntityParamsType.MP).Value;

        SubButtonChooseState subChooseState = (SubButtonChooseState)this.Controller.GetGameState();
        if (mp < Mp())
        {
            SomeTextState textReturnState = new SomeTextState(new string[] { "MPが足りません。" }, subChooseState);
            subChooseState.NextState = textReturnState;
            return;
        }

        UIController uic = this.Controller.GetComponent<UIController>();
        uic.HideAllButton();

        //SubButtonState -> SomeTextState -> DiceState
        RollingDiceState diceState = new RollingDiceState(subChooseState, this.Dice);
        SomeTextState textState = new SomeTextState(new string[] { GetItemName() + "を使った！" }, diceState);
        subChooseState.NextState = textState;
    }

    public new string GetItemDescription()
    {
        return base.GetItemDescription()+"(消費MP："+Mp()+")";
    }

    public int Mp()
    {
        return 10;
    }
}

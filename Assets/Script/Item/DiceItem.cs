using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceItem : IItem
{
    protected GameController Controller;
    protected Dice Dice;
    private string name;

    public DiceItem(GameController Controller, Dice Dice, string name)
    {
        this.Controller = Controller;
        this.Dice = Dice;
        this.name = name;
    }
    public void Action()
    {
        UIController uic = this.Controller.GetComponent<UIController>();
        uic.HideAllButton();

        //SubButtonState -> SomeTextState -> DiceState
        SubButtonChooseState subChooseState = (SubButtonChooseState)this.Controller.GetGameState();
        RollingDiceState diceState = new RollingDiceState(subChooseState, this.Dice);
        SomeTextState textState = new SomeTextState(new string[] { GetItemName() + "を使った！" }, diceState);
        subChooseState.NextState = textState;
    }

    public string GetItemName()
    {
        return this.name;
    }
    public string GetItemDescription()
    {
        return this.Dice.GetName() + "ふることができます。";
    }
    public bool IsAvailableInBattle()
    {
        return true;
    }
    public bool IsAvailableInMap()
    {
        return true;
    }
    public bool IsAvailableInShop()
    {
        return false;
    }
}

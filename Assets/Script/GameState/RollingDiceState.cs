using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class RollingDiceState : IGameState{

    private UIController UIController;
    private IGameState PrevState;

    private Sprite[] DiceSprites;
    private Image[] DiceImage;
    private bool IsRolling;
    private int[] Result;
    private int[] DiceIndexs;
    private int TotalDiceCount;

    public RollingDiceState(IGameState PrevState, Dice Dice)
    {
        this.PrevState = PrevState;
        this.Result = Dice.GenerateRandom();
        this.DiceIndexs = Dice.GetDiceIndex(this.Result.Length);
    }
    /**
     * さいころをふる状態
     * 移動/攻撃へ遷移
     */
    public IGameState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.UIController = Controller.GetUIController();
            this.DiceSprites = Resources.LoadAll<Sprite>("Texture.Dice");
            this.DiceImage = this.UIController.DicePanel;
            this.IsRolling = true;
            for (int i = 0; i < this.DiceIndexs.Length; i++)
            {
                this.UIController.SetDicePanelVisible(this.DiceIndexs[i], true);
                this.TotalDiceCount += this.Result[i];
            }
            //さいころルーチンスタート
            this.UIController.StartCoroutine(this.RollDice());
        }

        //結果表示　移動/攻撃状態へ遷移
        if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            this.IsRolling = false;
            UIController.Log(this.TotalDiceCount + "が出た！");

            if (this.PrevState.GetType() == typeof(ButtonChooseState))
            {
                return new DiceMoveState(this.TotalDiceCount);
            }
            else if (this.PrevState.GetType() == typeof(SubItemButtonChooseState))
            {
                //スキルの場合、MP消費
                //道具の場合、道具消費
                SubItemButtonChooseState sbState = (SubItemButtonChooseState)this.PrevState;
                if (sbState.GetSelectedIItem() is ISkill)
                {
                    ISkill skill = (ISkill) sbState.GetSelectedIItem();
                    Controller.GetCurrentPlayer().GetParameta(EntityParamsType.MP).AddValue(-skill.Mp());
                }
                else
                {
                    sbState.RemoveItem();
                }

                if (sbState.IsButtonChooseState())
                {
                    return new DiceMoveState(this.TotalDiceCount);
                }
                else if (sbState.IsBattleeState())
                {
                    return new BattleDamageState(this.TotalDiceCount, (BattleState)sbState.PrevState);
                }
            }
            else if (this.PrevState.GetType() == typeof(BattleState))
            {
                return new BattleDamageState(this.TotalDiceCount, (BattleState)this.PrevState);
            }
        }
        //cancel dice
        else if (Player.GetButtonDown(Player.GamePadBoolKey.B))
        {
            this.IsRolling = false;
            this.UIController.SetDicePanelVisible(false);

            if (this.PrevState.GetType() == typeof(ButtonChooseState) || this.PrevState.GetType() == typeof(BattleState))
            {
                ((ButtonChooseState)this.PrevState).SetButtonVisible(true);
                return this.PrevState;
            }
            else if (this.PrevState.GetType() == typeof(SubItemButtonChooseState))
            {
                ((SubButtonChooseState)this.PrevState).SetButtonVisible(true);
                return this.PrevState;
            }
        }
        return this;
    }

    private IEnumerator RollDice()
    {
        int[] diceCount = new int[this.DiceIndexs.Length];
        for (int i = 0; i < this.DiceIndexs.Length; i++)
        {
            diceCount[i] = 0;
        }
        while (this.IsRolling)
        {
            for (int i = 0; i < this.DiceIndexs.Length; i++)
            {
                diceCount[i] = (diceCount[i] + Random.Range(1, 4)) % 6;
                this.DiceImage[this.DiceIndexs[i]].sprite = this.DiceSprites[diceCount[i]];
            }

            yield return new WaitForSeconds(0.07f);
        }

        for (int i = 0; i < this.DiceIndexs.Length; i++)
        {
            this.DiceImage[this.DiceIndexs[i]].sprite = this.DiceSprites[this.Result[i] - 1];
        }
    }
}

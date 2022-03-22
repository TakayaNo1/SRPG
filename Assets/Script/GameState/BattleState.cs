using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

class BattleState : ButtonChooseState
{
    /**
     * デバッグ用
     * 敵のレベル
     */
    public static int EnemyLevel = 1;

    private PlayerStatus PlayerStatus;
    private List<EnemyStatus> Enemys = new List<EnemyStatus>();
    public BattleState() : base()
    {
        Enemys.Add(new EnemyStatus("Slime", EnemyLevel++));
    }
    public BattleState(EnemyStatus Enemy) : base()
    {
        this.Enemys.Add(Enemy);
    }
    public BattleState(EnemyStatus[] Enemys) : base()
    {
        this.Enemys.AddRange(Enemys);
    }

    /**
     * バトル選択肢状態
     */
    public override IGameState Next(GameController Controller)
    {
        if (base.UIController == null)
        {
            base.UIController = Controller.GetUIController();
            base.UIController.SetBattlePanelVisible(true);
            base.Button = base.UIController.SelectButton;
            base.ButtonActions = new UnityAction[this.Button.Length];
            this.InitButton(Controller);
            
            this.PlayerStatus = Controller.GetCurrentPlayer().GetPlayerStatus();
            base.UIController.GetBattleUIController().SetEnemyImage(this.Enemys);

            string[] texts = new string[this.Enemys.Count];
            for(int i=0;i< texts.Length; i++)
            {
                texts[i] = this.Enemys[i].Name+"(LVL:"+ (this.Enemys[i].Params[(int)EntityParamsType.LEVEL].Value-1) + ")が現れた！";
            }

            return new SomeTextState(texts, this);
        }

        if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_UP))
        {
            base.ButtonUp();
        }
        else if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_DOWN))
        {
            base.ButtonDown();
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            base.ButtonInvoke();

            if (base.NextState != null)
            {
                return base.NextState;
            }
        }

        return this;
    }

    public List<EnemyStatus> GetEnemys()
    {
        return this.Enemys;
    }
    protected override void InitButton(GameController Controller)
    {
        base.SetButton(new string[] {"たたかう", "スキル", "アイテム", "逃げる"});
        base.UIController.HideSubButton();
        base.SetButtonListener(0, FightButton);
        base.SetButtonListener(1, () => { SkillButton(Controller); });
        base.SetButtonListener(2, () => { ItemButton(Controller); });
        base.SetButtonListener(3, RunButton);
        base.SetButtonListener(4, () => { });
        base.SetButtonListener(5, () => { });
    }
    private void FightButton()
    {
        base.SetButton(null);
        this.SetNextState(new RollingDiceState(this, new Dice(1)));
    }
    private void SkillButton(GameController Controller)
    {
        List<IItem> skills = Controller.GetCurrentPlayer().GetPlayerStatus().Skills;
        if (skills.Count == 0)
        {
            this.SetNextState(new SomeTextState(new string[] { "スキルを持っていません！" }, this));
            return;
        }

        this.SetNextState(new SubItemButtonChooseState(this, skills));
    }
    private void ItemButton(GameController Controller)
    {
        List<IItem> items = Controller.GetCurrentPlayer().GetPlayerStatus().Items;
        if (items.Count == 0)
        {
            this.SetNextState(new SomeTextState(new string[] { "アイテムを持っていません！" }, this));
            return;
        }

        this.SetNextState(new SubItemButtonChooseState(this, items));
    }
    private void RunButton()
    {
        int player_lvl = this.PlayerStatus.Params[(int)EntityParamsType.LEVEL].Value;
        int enemy_max_lvl = 0;
        foreach (EnemyStatus enemy in this.Enemys)
        {
            int lvl = enemy.Params[(int)EntityParamsType.LEVEL].Value;
            if (lvl > enemy_max_lvl) enemy_max_lvl = lvl;
        }
        int gap = player_lvl - enemy_max_lvl;
        int value;

        if (gap < -10) value = 10;
        else if (-10 <= gap && gap < 0) value = 70 + gap * 6;
        else if (0 <= gap && gap < 10) value = 70 + gap * 2;
        else if (10 <= gap && gap < 20) value = 80 + gap;
        else value = 100;

        if (value >= Random.Range(0, 100))
        {
            this.SetNextState(new SomeTextState(new string[] { "上手く逃げ切れた！" }, new BattleFinState()));
            EnemyLevel--;
        }
        else
        {
            this.SetNextState(new SomeTextState(new string[] { "逃げるのに失敗した。" }, new BattleDamageState(0, this)));
            base.SetButton(null);
        }
    }
}

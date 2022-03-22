using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattleDamageState : IGameState
{
    private readonly BattleState NextState;
    private DamageStateType StateType;
    private bool IsFirst;

    private PlayerStatus PlayerStatus;
    private EntityStatus FromEntity, ToEntity;
    private int SelectedEnemyIndex;
    private int PlayerDiceCount, EnemyDiceCount;

    private List<EnemyStatus> KilledEnemys=new List<EnemyStatus>();
    public BattleDamageState(int DiceCount, BattleState NextState)
    {
        this.PlayerDiceCount = DiceCount;
        this.NextState = NextState;
        this.StateType = DamageStateType.BeforeDamage;
        this.IsFirst = true;
    }
    /**
     * バトル状態
     */
    public IGameState Next(GameController Controller)
    {
        if (this.FromEntity == null)
        {
            this.PlayerStatus = Controller.GetCurrentPlayer().GetPlayerStatus();
            this.SelectedEnemyIndex = 0;

            if (this.PlayerDiceCount == 0)//逃げる失敗し、敵のみ攻撃する
            {
                this.ToEntity = this.PlayerStatus;
                this.FromEntity = this.NextState.GetEnemys()[this.SelectedEnemyIndex];
                this.IsFirst = false;
                this.StateType = DamageStateType.EntityDamaged;
                this.EnemyDiceCount = Random.Range(1, 7);
                return new SomeTextState(new string[] { this.NextState.GetEnemys()[this.SelectedEnemyIndex].Name + "は" + this.EnemyDiceCount + "が出た！" }, this);
            }
            else if (IsPlayerOrderFirst())//プレイヤーが始めに攻撃するか
            {
                this.FromEntity = this.PlayerStatus;
                this.ToEntity = this.NextState.GetEnemys()[this.SelectedEnemyIndex];
            }
            else
            {
                this.ToEntity = this.PlayerStatus;
                this.FromEntity = this.NextState.GetEnemys()[this.SelectedEnemyIndex];
            }
        }
        
        if (this.StateType == DamageStateType.BeforeDamage)//サイコロの結果表示
        {
            this.StateType = DamageStateType.EntityDamaged;

            if (this.IsFirst)
            {
                List<string> lines = new List<string>();
                this.EnemyDiceCount = Random.Range(1, 7);
                lines.Add(this.PlayerDiceCount + "が出た！");
                lines.Add(this.NextState.GetEnemys()[this.SelectedEnemyIndex].Name + "は" + this.EnemyDiceCount + "が出た！");
                return new SomeTextState(lines, this);
            }

            return this;
        }
        else if (this.StateType == DamageStateType.EntityDamaged)//攻撃
        {
            List<string> lines = new List<string>();
            lines.Add(this.FromEntity.Name + "の攻撃！");

            if (this.ToEntity.GetType() == typeof(PlayerStatus))
            {
                Controller.GetUIController().GetBattleUIController().DamagedPlayer();
            }
            else if (this.ToEntity.GetType() == typeof(EnemyStatus))
            {
                Controller.GetUIController().GetBattleUIController().DamagedEntity(this.SelectedEnemyIndex);
            }

            this.StateType = DamageStateType.AfterDamage;
            return new SomeTextState(lines, this);
        }
        else if (this.StateType == DamageStateType.AfterDamage)//攻撃結果
        {
            if (this.IsFirst)
            {
                Controller.GetUIController().SetDicePanelVisible(false);
            }

            EntityStatus.DamageInfo info;
            if (this.FromEntity.GetType() == typeof(PlayerStatus))
            {
                info = this.ToEntity.Damage(this.FromEntity, this.PlayerDiceCount);
            }
            else
            {
                info = this.ToEntity.Damage(this.FromEntity, this.EnemyDiceCount);
            }
            List<string> lines = new List<string>();

            lines.Add(this.ToEntity.Name + "は" + info.Damage + "ダメージをうけた。");

            if (info.RemainHP <= 0)
            {
                lines.Add(this.ToEntity.Name + "は力尽きた。");

                if (this.ToEntity.GetType() == typeof(PlayerStatus))
                {
                    return new SomeTextState(lines, new DeathState());
                }

                this.KilledEnemys.Add((EnemyStatus)this.ToEntity);
                this.NextState.GetEnemys().Remove((EnemyStatus)this.ToEntity);
                this.StateType = DamageStateType.Exp;
                return new SomeTextState(lines, this);
            }

            if (!this.IsFirst)
            {
                this.StateType = DamageStateType.ContinueBattle;
                return new SomeTextState(lines, this);
            }

            this.StateType = DamageStateType.BeforeDamage;
            this.IsFirst = false;
            this.SwapEntity();

            return new SomeTextState(lines, this);
        }
        else if (this.StateType == DamageStateType.ContinueBattle)//次の攻撃ターン
        {
            this.NextState.SetButtonVisible(true);
            return this.NextState;
        }
        else if (this.StateType == DamageStateType.Exp)//経験値、バトル終了
        {
            int exp = this.ToEntity.Params[(int)EntityParamsType.EXP].Value;
            List<string> lines = new List<string>();
            lines.Add(exp + "EXPを得た！");
            int level = Controller.GetCurrentPlayer().GetPlayerStatus().Exp(exp);
            if (level > 0)
            {
                lines.Add("レベルが" + level + "上がった。");
            }
            return new SomeTextState(lines, new BattleFinState(this.KilledEnemys));
        }

        return this;
    }
    //攻撃順番の交代
    private void SwapEntity()
    {
        EntityStatus tmp = this.FromEntity;
        this.FromEntity = this.ToEntity;
        this.ToEntity = tmp;
    }
    //攻撃順番の計算
    private bool IsPlayerOrderFirst()
    {
        int player_spd = this.PlayerStatus.Params[(int)EntityParamsType.SPD].Value;
        int enemy_spd = this.NextState.GetEnemys()[this.SelectedEnemyIndex].Params[(int)EntityParamsType.SPD].Value;
        int gap = player_spd - enemy_spd;
        int value;

        if (gap <= -10) value = 0;
        else if (gap > 10) value = 100;
        else value = 50 + gap * 5;

        return value >= Random.Range(0, 100);
    }

    private enum DamageStateType
    {
        BeforeDamage, EntityDamaged, AfterDamage, ContinueBattle, Exp
    }
}

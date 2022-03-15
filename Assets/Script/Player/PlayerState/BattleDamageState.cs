using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BattleDamageState : IPlayerState
{
    private BattleState NextState;
    private DamageStateType StateType;
    private bool IsFirst;

    private PlayerStatus PlayerStatus;
    private EntityStatus FromEntity, ToEntity;
    private int SelectedEnemyIndex;
    private int PlayerDiceCount, EnemyDiceCount;
    public BattleDamageState(int DiceCount, BattleState NextState)
    {
        this.PlayerDiceCount = DiceCount;
        this.NextState = NextState;
        this.StateType = DamageStateType.BeforeDamage;
        this.IsFirst = true;
    }
    public IPlayerState Next(GameController Controller)
    {
        if (this.FromEntity == null)
        {
            this.PlayerStatus = Controller.GetCurrentPlayer().GetPlayerStatus();
            this.SelectedEnemyIndex = 0;
            //Debug.Log(this.PlayerStatus.Enemys[this.SelectedEnemyIndex].Status[(int)EntityStatusType.HP].Value);
            if (this.PlayerDiceCount == 0)
            {
                this.ToEntity = this.PlayerStatus;
                this.FromEntity = this.PlayerStatus.Enemys[this.SelectedEnemyIndex];
                this.IsFirst = false;
                this.StateType = DamageStateType.EntityDamaged;
                this.EnemyDiceCount = Random.Range(1, 7);
                return new SomeTextState(new string[] { this.PlayerStatus.Enemys[this.SelectedEnemyIndex].Name + "は" + this.EnemyDiceCount + "が出た！" }, this);
            }
            else if (IsPlayerOrderFirst())
            {
                this.FromEntity = this.PlayerStatus;
                this.ToEntity = this.PlayerStatus.Enemys[this.SelectedEnemyIndex];
            }
            else
            {
                this.ToEntity = this.PlayerStatus;
                this.FromEntity = this.PlayerStatus.Enemys[this.SelectedEnemyIndex];
            }
        }

        if (this.StateType == DamageStateType.BeforeDamage)
        {
            this.StateType = DamageStateType.EntityDamaged;

            if (this.IsFirst)
            {
                List<string> lines = new List<string>();
                this.EnemyDiceCount = Random.Range(1, 7);
                lines.Add(this.PlayerDiceCount + "が出た！");
                lines.Add(this.PlayerStatus.Enemys[this.SelectedEnemyIndex].Name + "は" + this.EnemyDiceCount + "が出た！");
                return new SomeTextState(lines, this);
            }

            return this;
        }
        else if (this.StateType == DamageStateType.EntityDamaged)
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
        else if (this.StateType == DamageStateType.AfterDamage)
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

                this.PlayerStatus.Enemys.Remove((EnemyStatus)this.ToEntity);
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
        else if (this.StateType == DamageStateType.ContinueBattle)
        {
            this.NextState.SetButtonVisible(true);
            return this.NextState;
        }
        else if (this.StateType == DamageStateType.Exp)
        {
            int exp = this.ToEntity.Params[(int)EntityParamsType.EXP].Value;
            List<string> lines = new List<string>();
            lines.Add(exp + "EXPを得た！");
            int level = Controller.GetCurrentPlayer().GetPlayerStatus().Exp(exp);
            if (level > 0)
            {
                lines.Add("レベルが" + level + "上がった。");
            }
            return new SomeTextState(lines, new BattleFinState());
        }

        return this;
    }

    private void SwapEntity()
    {
        EntityStatus tmp = this.FromEntity;
        this.FromEntity = this.ToEntity;
        this.ToEntity = tmp;
    }

    private bool IsPlayerOrderFirst()
    {
        int player_spd = this.PlayerStatus.Params[(int)EntityParamsType.SPD].Value;
        int enemy_spd = this.PlayerStatus.Enemys[this.SelectedEnemyIndex].Params[(int)EntityParamsType.SPD].Value;
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

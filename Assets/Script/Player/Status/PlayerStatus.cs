using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStatus : EntityStatus
{
    public PlayerStatus(): base()
    {
        this.Enemys = new List<EnemyStatus>();
        base.SetParams(1);
        this.Params[(int)EntityParamsType.EXP].Value = 0;
    }
    public IPlayerState State
    {
        get; set;
    }
    public List<EnemyStatus> Enemys
    {
        get; set;
    }
    private ArmorItem[] Armors = new ArmorItem[4];
    private int[] Bufs = new int[Enum.GetValues(typeof(EntityParamsType)).Length];
    public void SetArmorItem(ArmorItem Item)
    {
        this.Armors[(int)Item.GetArmorType()] = Item;

        int[] buf = new int[Enum.GetValues(typeof(EntityParamsType)).Length];
        for (int i = 0; i < this.Armors.Length; i++)
        {
            int[] buf2 = this.Armors[i].GetBuf();
            foreach (EntityParamsType key in Enum.GetValues(typeof(EntityParamsType)))
            {
                buf[(int)key] += buf2[(int)key];
            }
        }
        this.Bufs = buf;
    }

    protected override int GetParamsValue(EntityParamsType Type, int Level)
    {
        if (Type == EntityParamsType.LEVEL) return Level;
        else if (Type == EntityParamsType.EXP) return 70 + (int)(Mathf.Pow(Level, 2.1f) * 10f);         //80 ~ 160000
        else if (Type == EntityParamsType.HP) return 48 + (int)(Mathf.Pow(Level, 1.8f) * 2.6f);         //50 ~ 10000
        else if (Type == EntityParamsType.MP) return 9 + (int)(Mathf.Pow(Level, 1.7f) * 1.0f);          //10 ~ 2500
        else if (Type == EntityParamsType.ATT) return -4 + (int)(Mathf.Pow(Level + 7, 1.9f) * 0.16f);   //4 ~ 30 ~ 1200
        else if (Type == EntityParamsType.DEF) return -2 + (int)(Mathf.Pow(Level + 7, 2.1f) * 0.07f);   //3 ~ 25 ~ 1200
        else if (Type == EntityParamsType.SPD) return 1 + (int)(Mathf.Pow(Level + 7, 2.2f) * 0.035f);   // 4 ~ 19 ~ 1000
        return base.GetParamsValue(Type, Level);
    }
    private void LevelUp()
    {
        base.Params[(int)EntityParamsType.LEVEL].AddMaxValue(1);
        base.Params[(int)EntityParamsType.LEVEL].AddValue(1);
        int lvl = base.Params[(int)EntityParamsType.LEVEL].Value;
        base.Params[(int)EntityParamsType.EXP].Value = 0;

        base.Params[(int)EntityParamsType.EXP].MaxValue = GetParamsValue(EntityParamsType.EXP, lvl);
        base.Params[(int)EntityParamsType.HP].Value = GetParamsValue(EntityParamsType.HP, lvl);
        base.Params[(int)EntityParamsType.HP].MaxValue = GetParamsValue(EntityParamsType.HP, lvl);
        base.Params[(int)EntityParamsType.MP].Value = GetParamsValue(EntityParamsType.MP, lvl);
        base.Params[(int)EntityParamsType.MP].MaxValue = GetParamsValue(EntityParamsType.MP, lvl);
        base.Params[(int)EntityParamsType.ATT].Value = GetParamsValue(EntityParamsType.ATT, lvl);
        base.Params[(int)EntityParamsType.ATT].MaxValue = GetParamsValue(EntityParamsType.ATT, lvl);
        base.Params[(int)EntityParamsType.DEF].Value = GetParamsValue(EntityParamsType.DEF, lvl);
        base.Params[(int)EntityParamsType.DEF].MaxValue = GetParamsValue(EntityParamsType.DEF, lvl);
        base.Params[(int)EntityParamsType.SPD].Value = GetParamsValue(EntityParamsType.SPD, lvl);
        base.Params[(int)EntityParamsType.SPD].MaxValue = GetParamsValue(EntityParamsType.SPD, lvl);
    }
    public int Exp(int Value)
    {
        int lvl = 0;
        while ((Value = base.Params[(int)EntityParamsType.EXP].AddValue(Value)) >= 0)
        {
            this.LevelUp();
            lvl++;
            if (Value == 0) break;
        }
        return lvl;
    }

    override public string ToString()
    {
        string str = this.Name + "\n";
        foreach (EntityParamsType key in Enum.GetValues(typeof(EntityParamsType)))
        {
            if (key == EntityParamsType.LEVEL)
            {
                str += Enum.GetName(typeof(EntityParamsType), key) + " : " + this.Params[(int)key].Value + "\n";
            }
            else if (key == EntityParamsType.ATT || key == EntityParamsType.DEF || key == EntityParamsType.SPD)
            {
                str += Enum.GetName(typeof(EntityParamsType), key) + " : " + this.Params[(int)key].Value;
                if (this.Bufs[(int)key] != 0)
                {
                    str += "(" + this.Bufs[(int)key] + ")";
                }
                str += "\n";
            }
            else
            {
                str += Enum.GetName(typeof(EntityParamsType), key) + " : " + this.Params[(int)key].Value + "/" + this.Params[(int)key].MaxValue;
                if (this.Bufs[(int)key] != 0)
                {
                    str += "(" + this.Bufs[(int)key] + ")";
                }
                str += "\n";
            }
        }
        return str;
    }
}
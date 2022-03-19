using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Slime : EnemyStatus
{
    public Slime(string Name, int Level) : base(Name, Level)
    {
        base.Name = Name;
        base.SetParams(Level);
    }

    protected override int GetParamsValue(EntityParamsType Type, int Level)
    {
        if (Type == EntityParamsType.LEVEL) return Level + 1;
        else if (Type == EntityParamsType.EXP) return 20 + (int)(Mathf.Pow(Level, 1.55f) * 1.5f);       //20 ~ 2000 (80 ~ 160000)
        else if (Type == EntityParamsType.HP) return 20 + (int)(Mathf.Pow(Level, 1.8f) * 2.1f);         //20 ~ 8000 (50 ~ 10000)
        else if (Type == EntityParamsType.MP) return 1_0000_0000;                                       //inf(10 ~ 2500)
        else if (Type == EntityParamsType.ATT) return -3 + (int)(Mathf.Pow(Level + 7, 2.1f) * 0.08f);   //3 ~ 25 ~ 1500 (4 ~ 30 ~ 1200)
        else if (Type == EntityParamsType.DEF) return -4 + (int)(Mathf.Pow(Level + 7, 1.9f) * 0.13f);   //1 ~ 25 ~ 900 (3 ~ 25 ~ 1200)
        else if (Type == EntityParamsType.SPD) return -1 + (int)(Mathf.Pow(Level + 7, 2.2f) * 0.035f);  //2 ~ 19 ~ 1000 (4 ~ 19 ~ 1000)
        return base.GetParamsValue(Type, Level);
    }
}

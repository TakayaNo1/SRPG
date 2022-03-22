using System;
using System.Collections.Generic;

public class EntityStatus
{
    public EntityStatus()
    {
        Array arrays = Enum.GetValues(typeof(EntityParamsType));
        this.Params = new Parameta[arrays.Length];
        foreach (EntityParamsType type in arrays)
        {
            this.Params[(int)type] = new Parameta(0, 0);
        }

        this.GUID = Guid.NewGuid().ToString();
        this.Items = new List<IItem>();
        this.Skills = new List<IItem>();
    }
    public Parameta[] Params
    {
        get;
    }
    public string Name
    {
        get; set;
    }
    public string GUID
    {
        get;
    }
    public List<IItem> Items
    {
        get; set;
    }
    public List<IItem> Skills
    {
        get; set;
    }

    protected virtual int GetParamsValue(EntityParamsType Type, int Level)
    {
        return 0;
    }
    protected void SetParams(int Level)
    {
        Array arrays = Enum.GetValues(typeof(EntityParamsType));
        foreach (EntityParamsType type in arrays)
        {
            this.Params[(int)type].Value = this.GetParamsValue(type, Level);
            this.Params[(int)type].MaxValue = this.GetParamsValue(type, Level);
        }
    }

    //ダメージ計算
    public DamageInfo Damage(EntityStatus fromEntity, int DiceCount)
    {
        int att = fromEntity.Params[(int)EntityParamsType.ATT].Value;
        int def = this.Params[(int)EntityParamsType.DEF].Value;
        int damage = (att * att * DiceCount) / (att + def);
        if (damage <= 0) damage = 1;
        int remain = -this.Params[(int)EntityParamsType.HP].AddValue(-damage);
        return new DamageInfo(damage, remain, fromEntity, this);
    }
    public struct DamageInfo
    {
        public DamageInfo(int Damage, int RemainHP, EntityStatus FromEntity, EntityStatus ToEntity)
        {
            this.Damage = Damage;
            this.RemainHP = RemainHP;
            this.FromEntity = FromEntity;
            this.ToEntity = ToEntity;
        }
        public int Damage { get; }
        public int RemainHP { get; }
        public EntityStatus FromEntity { get; }
        public EntityStatus ToEntity { get; }
    }

    override public string ToString()
    {
        string str = this.Name + "\n";
        foreach (EntityParamsType key in Enum.GetValues(typeof(EntityParamsType)))
        {
            if (key == EntityParamsType.LEVEL || key == EntityParamsType.ATT || key == EntityParamsType.DEF || key == EntityParamsType.SPD)
            {
                str += Enum.GetName(typeof(EntityParamsType), key) + " : " + this.Params[(int)key].Value + "\n";
            }
            else
            {
                str += Enum.GetName(typeof(EntityParamsType), key) + " : " + this.Params[(int)key].Value + "/" + this.Params[(int)key].MaxValue + "\n";
            }
        }
        return str;
    }
}
public enum EntityParamsType : int
{
    LEVEL = 0,
    EXP,
    HP,
    MP,
    ATT,
    DEF,
    SPD
}

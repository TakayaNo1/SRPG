using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Parameta
{
    public Parameta(int Value, int MaxValue)
    {
        this.MaxValue = MaxValue;
        this.Value = Value;
    }
    public Parameta(int MaxValue)
    {
        this.MaxValue = MaxValue;
        this.Value = MaxValue;
    }

    public int Value
    {
        set;
        get;
    }
    public int MaxValue
    {
        set;
        get;
    }
    public int AddValue(int Value)
    {
        int TotalValue = this.Value + Value;
        if (this.MaxValue <= TotalValue)
        {
            this.Value = this.MaxValue;
            return TotalValue - this.MaxValue;
        }
        else if(TotalValue < 0)
        {
            this.Value = 0;
            return -TotalValue;
        }

        this.Value = TotalValue;
        return -this.Value;
    }
    public void AddMaxValue(int Value)
    {
        this.MaxValue += Value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    private int DiceNumber; //サイコロの数
    private int RangeMin;   //下限
    private int RangeMax;   //上限

    public Dice(int DiceNumber)
    {
        this.DiceNumber = DiceNumber;
        this.RangeMin = 1;
        this.RangeMax = 6;
    }
    public Dice(int RangeMin, int RangeMax)
    {
        this.DiceNumber = 1;
        this.RangeMin = RangeMin;
        this.RangeMax = RangeMax;
    }

    public int[] GenerateRandom()
    {
        int[] result = new int[this.DiceNumber];
        for (int i = 0; i < DiceNumber; i++)
        {
            result[i] = (int)Random.Range(this.RangeMin, this.RangeMax + 1);
        }
        return result;
    }

    public string GetName()
    {
        return this.RangeMin+"から"+this.RangeMax+"までのさいころを"+this.DiceNumber+"個";
    }

    private static int[][] Index =
    {
        new int[]{ 8},
        new int[]{ 7, 8},
        new int[]{ 6, 7, 8},
        new int[]{ 4, 5, 7, 8},
        new int[]{ 4, 5, 6, 7, 8},
        new int[]{ 3, 4, 5, 6, 7, 8},
        new int[]{ 2, 3, 4, 5, 6, 7, 8},
        new int[]{ 1, 2, 3, 4, 5, 6, 7, 8},
        new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8},
    };
    public static int[] GetDiceIndex(int N)
    {
        return Index[N - 1];
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmorItem : IItem
{
    protected int[] Buf = new int[Enum.GetValues(typeof(EntityParamsType)).Length];
    protected ArmorType Type;
    /*
    public void Action()
    {

    }
    public string GetItemName()
    {
        return "";
    }
    public string GetItemDescription()
    {
        return "";
    }*/
    public abstract void Action();
    public abstract string GetItemName();
    public abstract string GetItemDescription();
    public bool IsAvailableInBattle()
    {
        return false;
    }
    public bool IsAvailableInMap()
    {
        return false;
    }
    public bool IsAvailableInShop()
    {
        return false;
    }
    public void SetBuf(EntityParamsType Type, int value)
    {
        this.Buf[(int)Type] = value;
    }
    public int[] GetBuf()
    {
        return this.Buf;
    }
    public ArmorType GetArmorType()
    {
        return this.Type;
    }
}

public enum ArmorType : int
{
    HEAD = 0, CHEST, BOTTOM, LEG
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

abstract class SubButtonChooseState : IGameState
{

    public ButtonChooseState PrevState{ get; set;}
    public IGameState NextState { get; set; }

    protected int ButtonSize;
    protected int ButtonIndex;
    protected int SelectedItemIndex = 0;
    protected Button[] Button;
    protected List<UnityAction> ButtonActions;
    protected List<string> Items;

    public SubButtonChooseState(ButtonChooseState PrevState, List<string> Items, List<UnityAction> ButtonActions)
    {
        this.PrevState = PrevState;
        this.Items = Items;
        this.ButtonActions = ButtonActions;
        this.ButtonIndex = 0;
    }

    /**
     * サブ選択肢状態
     */
    public abstract IGameState Next(GameController Controller);
    public bool IsButtonChooseState()
    {
        return this.PrevState.GetType() == typeof(ButtonChooseState);
    }
    public bool IsBattleeState()
    {
        return this.PrevState.GetType() == typeof(BattleState);
    }
    public void SetButtonVisible(bool value)
    {
        this.PrevState.SetButtonVisible(value);
        for (int i = 0; i < this.ButtonSize; i++)
        {
            SetButtonVisible(i, value);
        }
    }
    protected string GetSelectedItem()
    {
        return this.Items[this.ButtonIndex * this.ButtonSize + this.SelectedItemIndex];
    }

    protected void ButtonLeft()
    {
        if (!SetButtons(--this.ButtonIndex))
        {
            ++this.ButtonIndex;
        }
    }
    protected void ButtonRight()
    {
        if (!SetButtons(++this.ButtonIndex))
        {
            --this.ButtonIndex;
        }
    }
    protected void ButtonUp()
    {
        int index = this.SelectedItemIndex;
        SetChooseButton(index++, false);

        if (index >= this.ButtonSize)
        {
            index = 0;
        }

        this.SelectedItemIndex = index;
        SetChooseButton(index, true);
    }
    protected void ButtonDown()
    {
        int index = this.SelectedItemIndex;
        SetChooseButton(index--, false);

        if (index < 0)
        {
            index = this.ButtonSize - 1;
        }

        this.SelectedItemIndex = index;
        SetChooseButton(index, true);
    }
    protected void ButtonInvoke()
    {
        this.ButtonActions[this.SelectedItemIndex].Invoke();
    }
    //アイテムボタンの選択
    protected bool SetButtons(int ButtonIndex)
    {
        if (ButtonIndex < 0)
        {
            return false;
        }

        int from = this.Button.Length * ButtonIndex;
        int to = this.Button.Length * (ButtonIndex + 1);
        
        if (to > this.Items.Count)
        {
            to = this.Items.Count;
        }
        
        this.ButtonSize = to - from;
        
        if (this.ButtonSize <= 0)
        {
            return false;
        }

        for (int i = 0; i < this.ButtonSize; i++)
        {
            SetButtonText(i, Items[from + i].ToString());
            SetButtonListener(i, ButtonActions[from + i]);
            SetButtonVisible(i, true);
            bool value = GetButtonInteractable(from + i);
            SetButtonInteractable(i, value);
        }
        for (int i = this.ButtonSize; i < this.Button.Length; i++)
        {
            SetButtonVisible(i, false);
        }

        this.SelectedItemIndex = 0;
        SetChooseButton(0, true);
        for (int i = 1; i < this.ButtonSize; i++)
        {
            SetChooseButton(i, false);
        }
        return true;
    }
    private void SetChooseButton(int index, bool value)
    {
        this.Button[index].image.color = value ? new Color(0.6f, 1f, 0.6f, 1) : Color.white;
    }
    private void SetButtonVisible(int index, bool value)
    {
        this.Button[index].gameObject.SetActive(value);
    }
    protected virtual bool GetButtonInteractable(int index)
    {
        return true;
    }
    private void SetButtonInteractable(int index, bool value)
    {
        this.Button[index].interactable = value;
    }
    private void SetButtonText(int index, string value)
    {
        this.Button[index].GetComponentInChildren<Text>().text = value;
    }
    protected void SetButtonListener(int index, UnityAction action)
    {
        this.ButtonActions[index] = action;
    }
}

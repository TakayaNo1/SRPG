using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

class SubItemButtonChooseState : SubButtonChooseState
{
    private UIController UIController;
    private List<IItem> Items;

    public SubItemButtonChooseState(ButtonChooseState PrevState, List<IItem> Items) : base(PrevState, ToStringList(Items), ToActionList(Items))
    {
        this.Items = Items;
    }
    private static List<string> ToStringList(List<IItem> Items)
    {
        List<string> a = new List<string>();
        foreach(IItem item in Items)
        {
            a.Add(item.GetItemName());
        }
        return a;
    }
    private static List<UnityAction> ToActionList(List<IItem> Items)
    {
        List<UnityAction> a = new List<UnityAction>();
        foreach (IItem item in Items)
        {
            a.Add(item.Action);
        }
        return a;
    }

    public override IPlayerState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.UIController = Controller.GetUIController();
            this.Button = this.UIController.SubButton;
            SetButton(0);
            UIController.Log(this.GetSelectedIItem().GetItemDescription());
        }

        if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_UP))
        {
            ButtonUp();
            UIController.Log(this.GetSelectedIItem().GetItemDescription());
        }
        else if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_DOWN))
        {
            ButtonDown();
            UIController.Log(this.GetSelectedIItem().GetItemDescription());
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            ButtonInvoke();

            if (this.NextState != null)
            {
                return this.NextState;
            }
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.B))
        {
            this.UIController.HideSubButton();
            UIController.Log("");
            return this.PrevState;
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.L))
        {
            ButtonLeft();
            UIController.Log(this.GetSelectedIItem().GetItemDescription());
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.R))
        {
            ButtonRight();
            UIController.Log(this.GetSelectedIItem().GetItemDescription());
        }

        return this;
    }

    public IItem GetSelectedIItem()
    {
        return this.Items[this.ButtonIndex * this.ButtonSize + this.SelectedItemIndex];
    }
    protected override bool GetButtonInteractable(int index)
    {
        bool value = this.PrevState.GetType() == typeof(ButtonChooseState) && Items[index].IsAvailableInMap();
        value |= this.PrevState.GetType() == typeof(BattleState) && Items[index].IsAvailableInBattle();
        return value;
    }
    public void RemoveItem()
    {
        this.Items.Remove(this.GetSelectedIItem());
    }
    /*
    private UIController UIController;
    private ButtonChooseState PrevState;
    private IPlayerState NextState;

    private int ButtonSize;
    private int ButtonIndex;
    private int SelectedItemIndex = 0;
    private Button[] Button;
    private List<IItem> Items;

    public SubItemButtonChooseState(ButtonChooseState PrevState, List<IItem> Items)
    {
        this.PrevState = PrevState;
        this.Items = Items;
        this.ButtonIndex = 0;
    }

    public IPlayerState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.UIController = Controller.GetUIController();
            this.Button = this.UIController.SubButton;
            SetButton(0);
            UIController.Log(this.GetSelectedItem().GetItemDescription());
        }

        if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_UP))
        {
            ButtonUp();
            UIController.Log(this.GetSelectedItem().GetItemDescription());
        }
        else if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_DOWN))
        {
            ButtonDown();
            UIController.Log(this.GetSelectedItem().GetItemDescription());
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            ButtonInvoke();

            if (this.NextState != null)
            {
                return this.NextState;
            }
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.B))
        {
            this.UIController.HideSubButton();
            UIController.Log("");
            return this.PrevState;
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.L))
        {
            ButtonLeft();
            UIController.Log(this.GetSelectedItem().GetItemDescription());
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.R))
        {
            ButtonRight();
            UIController.Log(this.GetSelectedItem().GetItemDescription());
        }

        return this;
    }

    public void SetNextState(IPlayerState NextState)
    {
        this.NextState = NextState;
    }
    public ButtonChooseState GetPrevState()
    {
        return this.PrevState;
    }
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
    public void RemoveItem()
    {
        this.Items.Remove(this.GetSelectedItem());
    }
    private IItem GetSelectedItem()
    {
        return this.Items[this.ButtonIndex * this.ButtonSize + this.SelectedItemIndex];
    }

    private void ButtonLeft()
    {
        if (!SetButton(--this.ButtonIndex))
        {
            ++this.ButtonIndex;
        }
    }
    private void ButtonRight()
    {
        if (!SetButton(++this.ButtonIndex))
        {
            --this.ButtonIndex;
        }
    }
    private void ButtonUp()
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
    private void ButtonDown()
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
    private void ButtonInvoke()
    {
        if (this.Button[this.SelectedItemIndex].interactable)
        {
            IItem item = this.Items[this.ButtonIndex * this.ButtonSize + this.SelectedItemIndex];
            item.Action();
            //this.Button[this.SelectedItemIndex].onClick.Invoke();
        }
    }

    private bool SetButton(int ButtonIndex)
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
            SetButtonText(i, Items[from + i].GetItemName());
            SetButtonListener(i, Items[from + i].Action);
            SetButtonVisible(i, true);
            bool value = this.PrevState.GetType() == typeof(ButtonChooseState) && Items[from + i].IsAvailableInMap();
            value |= this.PrevState.GetType() == typeof(BattleState) && Items[from + i].IsAvailableInBattle();
            //value |= this.PrevState is ButtonChooseState && Items[from + i].IsAvailableInMap();
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
    private void SetButtonInteractable(int index, bool value)
    {
        this.Button[index].interactable = value;
    }
    private void SetButtonText(int index, string value)
    {
        this.Button[index].GetComponentInChildren<Text>().text = value;
    }
    private void SetButtonListener(int index, UnityAction action)
    {
        this.Button[index].onClick.RemoveAllListeners();
        this.Button[index].onClick.AddListener(action);
    }
    */
}

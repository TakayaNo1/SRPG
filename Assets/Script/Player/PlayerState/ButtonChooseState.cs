using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

class ButtonChooseState : IPlayerState{

    protected UIController UIController;
    protected IPlayerState NextState;

    private int ButtonSize;
    private int SelectedItemIndex = 0;
    protected Button[] Button;
    private UnityAction[] ButtonActions = new UnityAction[6];

    public ButtonChooseState()
    {
    }

    public virtual IPlayerState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.UIController = Controller.GetUIController();
            this.Button = this.UIController.SelectButton;
            this.InitButton(Controller);
        }

        if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_UP))
        {
            ButtonUp();
        }
        else if (Player.GetDPADButtonDown(Player.GamePadDPADKey.DPAD_DOWN))
        {
            ButtonDown();
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            ButtonInvoke();

            if (this.NextState != null)
            {
                return this.NextState;
            }
        }
        return this;
    }
    public void SetNextState(IPlayerState NextState)
    {
        this.NextState = NextState;
    }
    public void SetButtonVisible(bool value)
    {
        for (int i = 0; i < this.ButtonSize; i++)
        {
            SetButtonVisible(i, value);
        }
    }

    protected virtual void InitButton(GameController Controller)
    {
        SetButton(new string[] { "進む", "アイテム", "スキル", "装備", "マップ", "設定" });
        this.UIController.HideSubButton();
        SetButtonListener(0, SusumuButton);
        SetButtonListener(1, () => { ItemButton(Controller); });
        SetButtonListener(2, () => { SkillButton(Controller); });
        SetButtonListener(3, () => { ArmorButton(Controller); });
        SetButtonListener(4, () => { MapButton(Controller); });
        SetButtonListener(5, () => { SettingButton(Controller); });
    }
    private void SusumuButton()
    {
        SetButton(null);
        this.SetNextState(new RollingDiceState(this, new Dice(1)));
    }
    private void ItemButton(GameController Controller)
    {
        List<IItem> items = Controller.GetCurrentPlayer().GetPlayerStatus().Items;
        if (items.Count == 0)
        {
            this.SetNextState(new SomeTextState(new string[] { "アイテムを持っていません！" }, this));
            return;
        }

        this.SetNextState(new SubItemButtonChooseState(this, items));
    }
    private void SkillButton(GameController Controller)
    {
        List<IItem> skills = Controller.GetCurrentPlayer().GetPlayerStatus().Skills;
        if (skills.Count == 0)
        {
            this.SetNextState(new SomeTextState(new string[] { "スキルを持っていません！" }, this));
            return;
        }

        this.SetNextState(new SubItemButtonChooseState(this, skills));
    }
    private void ArmorButton(GameController Controller)
    {
        this.SetNextState(new SomeTextState(new string[] { "未実装" }, this));
    }
    private void MapButton(GameController Controller)
    {
        this.SetNextState(new SomeTextState(new string[] { "未実装" }, this));
    }
    private void SettingButton(GameController Controller)
    {
        this.SetNextState(new SubSettingButtonChooseState(this));
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
    protected void SetButton(string[] buttonNames)
    {
        if (buttonNames == null)
        {
            for (int i = 0; i < this.Button.Length; i++)
            {
                SetButtonVisible(i, false);
            }
            return;
        }

        this.ButtonSize = buttonNames.Length > this.Button.Length ? this.Button.Length : buttonNames.Length;
        for (int i = 0; i < this.ButtonSize; i++)
        {
            SetButtonVisible(i, true);
            SetButtonText(i, buttonNames[i]);

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
    }
    private void SetChooseButton(int index, bool value)
    {
        this.Button[index].image.color = value ? new Color(0.6f, 1f, 0.6f, 1) : Color.white;
    }
    private void SetButtonVisible(int index, bool value)
    {
        this.Button[index].gameObject.SetActive(value);
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

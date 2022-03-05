using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

class SubSettingButtonChooseState : SubButtonChooseState
{
    private UIController UIController;
    public SubSettingButtonChooseState(ButtonChooseState PrevState) : base(PrevState, new List<string> {"タイトル画面へ戻る"}, new List<UnityAction>(new UnityAction[1]))
    {
        SetButtonListener(0, () => SceneManager.LoadScene("HomeScene"));
    }

    public override IPlayerState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.UIController = Controller.GetUIController();
            this.Button = this.UIController.SubButton;
            SetButton(0);
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
        else if (Player.GetButtonDown(Player.GamePadBoolKey.B))
        {
            this.UIController.HideSubButton();
            return this.PrevState;
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.L))
        {
            ButtonLeft();
        }
        else if (Player.GetButtonDown(Player.GamePadBoolKey.R))
        {
            ButtonRight();
        }

        return this;
    }
}

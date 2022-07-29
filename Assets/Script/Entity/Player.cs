using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayableEntity
{
    public string Name;
    private PlayerStatus Status;

    override protected void Start()
    {
        base.Start();
        base.SetNameText(Name);

        base.CurrentSqare = MapGenerator.GetSquare(X, Z);
        base.Trans = GetComponent<Transform>();
        this.Status = new PlayerStatus();
        this.Status.Name = Name;
        this.Status.Skills.Add(new DiceSkill(base.GameController, new Dice(2), "さいころ×２"));

        MoveTo(this.CurrentSqare);
    }

    override protected void Update()
    {
        base.Update();
    }

    public PlayerStatus GetPlayerStatus()
    {
        return Status;
    }
    public override EntityStatus GetStatus()
    {
        return this.Status;
    }

    /**
     * DPAD入力方向のマス
     */
    public Square GetNextSquare()
    {
        if (base.CurrentSqare == null)
        {
            base.CurrentSqare = MapGenerator.GetSquare(X, Z);
        }
        if (GetDPADButtonDown(GamePadDPADKey.DPAD_RIGHT) && base.CurrentSqare.EastSquare != null)
        {
            return base.CurrentSqare.EastSquare;
        }
        else if (GetDPADButtonDown(GamePadDPADKey.DPAD_LEFT) && base.CurrentSqare.WestSquare != null)
        {
            return base.CurrentSqare.WestSquare;
        }
        else if (GetDPADButtonDown(GamePadDPADKey.DPAD_UP) && base.CurrentSqare.SouthSquare != null)
        {
            return base.CurrentSqare.SouthSquare;
        }
        else if (GetDPADButtonDown(GamePadDPADKey.DPAD_DOWN) && base.CurrentSqare.NorthSquare != null)
        {
            return base.CurrentSqare.NorthSquare;
        }
        return null;
    }

    private static bool DPADIsDown = false;
    public static bool GetDPADButtonDown(GamePadDPADKey Key)
    {
        Vector2 axis = GetAxis(GamePadAxisKey.DPAD);

        if (axis == Vector2.zero)
        {
            return DPADIsDown = false;
        }

        if (!DPADIsDown)
        {
            if (Key == GamePadDPADKey.DPAD_RIGHT && axis.x == 1)
            {
                return DPADIsDown = true;
            }
            else if (Key == GamePadDPADKey.DPAD_LEFT && axis.x == -1)
            {
                return DPADIsDown = true;
            }
            else if (Key == GamePadDPADKey.DPAD_UP && axis.y == 1)
            {
                return DPADIsDown = true;
            }
            else if (Key == GamePadDPADKey.DPAD_DOWN && axis.y == -1)
            {
                return DPADIsDown = true;
            }
        }
        return false;
    }
    public static bool GetButtonDown(GamePadBoolKey Key) { return Input.GetButtonDown(Key.ToString()); }
    public static bool GetButton(GamePadBoolKey Key) { return Input.GetButton(Key.ToString()); }
    public static Vector2 GetAxis(GamePadAxisKey Key)
    {
        float X = 0, Y = 0;

        //L_STICK,R_STICK,LRT,DPAD
        switch (Key)
        {
            case GamePadAxisKey.L_STICK:
                X = Input.GetAxis("L_STICK_X");
                Y = Input.GetAxis("L_STICK_Y");
                break;
            case GamePadAxisKey.R_STICK:
                X = Input.GetAxis("R_STICK_X");
                Y = Input.GetAxis("R_STICK_Y");
                break;
            case GamePadAxisKey.LRT:
                X = Input.GetAxis("LRT");
                Y = X;
                break;
            case GamePadAxisKey.DPAD:
                X = Input.GetAxis("DPAD_X");
                Y = Input.GetAxis("DPAD_Y");
                break;
        }

        if (X * X + Y * Y < 0.04)
        {
            X = 0;
            Y = 0;
        }

        return new Vector2(X, Y);
    }
    public enum GamePadBoolKey
    {
        A, B, X, Y, L, R, BACK, HOME, L_PRESS, R_PRESS
    }
    public enum GamePadAxisKey
    {
        L_STICK, R_STICK, LRT, DPAD
    }
    public enum GamePadDPADKey
    {
        DPAD_LEFT, DPAD_RIGHT, DPAD_UP, DPAD_DOWN
    }
}

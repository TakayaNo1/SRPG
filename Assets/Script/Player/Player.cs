using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameController GameController;

    public bool Order = false;

    public Square CurrentSqare;

    private Transform PlayerTrans;
    private PlayerStatus Status;

    void Start()
    {
        //this.CurrentSqare = this.StartSquare.GetComponent<Square>();
        this.CurrentSqare = MapGenerator.GetStartSquare();
        this.PlayerTrans = GetComponent<Transform>();
        this.Status = new PlayerStatus();
        this.Status.Name = "Player";
        this.Status.Skills.Add(new DiceSkill(GameController, new Dice(2), "さいころ×２"));

        //if (this.Order) this.Status.State = new ButtonChooseState();
        if (this.Order) this.Status.State = new StoryState1();
    }

    void Update()
    {
        if (this.Status.State != null)
        {
            this.Status.State = this.Status.State.Next(this.GameController); //state next
        }
        else
        {
            SceneManager.LoadScene("HomeScene");
        }
    }

    public PlayerStatus GetPlayerStatus()
    {
        return this.Status;
    }
    public Parameta GetParameta(EntityParamsType type)
    {
        return this.Status.Params[(int)type];
    }

    /**
     * Get入力方向のマス
     */
    public Square GetNextSquare()
    {
        if (this.CurrentSqare == null)
        {
            this.CurrentSqare = MapGenerator.GetStartSquare();
        }
        if (GetDPADButtonDown(GamePadDPADKey.DPAD_RIGHT) && this.CurrentSqare.EastSquare != null)
        {
            return this.CurrentSqare.EastSquare;
        }
        else if (GetDPADButtonDown(GamePadDPADKey.DPAD_LEFT) && this.CurrentSqare.WestSquare != null)
        {
            return this.CurrentSqare.WestSquare;
        }
        else if (GetDPADButtonDown(GamePadDPADKey.DPAD_UP) && this.CurrentSqare.SouthSquare != null)
        {
            return this.CurrentSqare.SouthSquare;
        }
        else if (GetDPADButtonDown(GamePadDPADKey.DPAD_DOWN) && this.CurrentSqare.NorthSquare != null)
        {
            return this.CurrentSqare.NorthSquare;
        }
        return null;
    }
    public void MoveToNextSquare(Square Next)
    {
        Vector3 dir = this.CurrentSqare.GetDirection(Next);
        Vector3 lookAtPos = this.PlayerTrans.position - new Vector3(dir.x, 0, dir.z);

        this.PlayerTrans.LookAt(lookAtPos);
        this.PlayerTrans.position += dir;

        this.CurrentSqare = Next;
    }

    private static bool DPADIsDown = false;
    public static bool GetDPADButtonDown(GamePadDPADKey Key)
    {
        Vector2 axis = GetAxis(GamePadAxisKey.DPAD);

        if (axis == Vector2.zero)
        {
            DPADIsDown = false;
            return false;
        }

        if (!DPADIsDown)
        {
            if (Key == GamePadDPADKey.DPAD_RIGHT && axis.x == 1)
            {
                DPADIsDown = true;
                return true;
            }
            else if (Key == GamePadDPADKey.DPAD_LEFT && axis.x == -1)
            {
                DPADIsDown = true;
                return true;
            }
            else if (Key == GamePadDPADKey.DPAD_UP && axis.y == 1)
            {
                DPADIsDown = true;
                return true;
            }
            else if (Key == GamePadDPADKey.DPAD_DOWN && axis.y == -1)
            {
                DPADIsDown = true;
                return true;
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
    public enum PlayerState : byte
    {
        MOVEMENT_STATE = 1,
        CUI_STATE = 2
    }
}

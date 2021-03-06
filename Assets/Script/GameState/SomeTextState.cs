using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SomeTextState : IGameState
{
    public static readonly string ClearText = "<ClearText>";

    private List<string> DisplayTexts;
    private List<string> QueueTexts;
    private IGameState NextState;

    public SomeTextState(string text, IGameState NextState)
    {
        this.DisplayTexts = new List<string>();
        this.QueueTexts = new List<string>(new string[]{ text });
        this.NextState = NextState;

        if (this.QueueTexts.Count > 0)
        {
            this.Display();
        }
    }
    public SomeTextState(string[] texts, IGameState NextState)
    {
        this.DisplayTexts = new List<string>();
        this.QueueTexts = new List<string>(texts);
        this.NextState = NextState;

        if (this.QueueTexts.Count > 0)
        {
            this.Display();
        }
    }
    public SomeTextState(List<string> texts, IGameState NextState)
    {
        this.DisplayTexts = new List<string>();
        this.QueueTexts = new List<string>(texts);
        this.NextState = NextState;

        if (this.QueueTexts.Count > 0)
        {
            this.Display();
        }
    }

    /**
     * テキストを順番に表示する状態
     */
    public IGameState Next(GameController Controller)
    {
        if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            if (this.QueueTexts.Count == 0)
            {
                UIController.Log("");
                return this.NextState;
            }

            this.Display();
        }
        return this;
    }

    private void Display()
    {
        string text = this.QueueTexts[0];
        this.QueueTexts.RemoveAt(0);

        if (text.StartsWith(ClearText))
        {
            this.DisplayTexts.Clear();
            text = text.Substring(ClearText.Length);
        }

        this.DisplayTexts.Add(text);

        if (this.DisplayTexts.Count > 3)
        {
            this.DisplayTexts.RemoveAt(0);
        }

        string displayText=string.Join("\n", this.DisplayTexts);

        UIController.Log(displayText);
    }
}

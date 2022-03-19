﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStoryState : IGameState
{
    private IGameState CurrentState;

    protected UIController UIController;
    protected StoryUIController StoryUIController;

    public IStoryState(IGameState CurrentState)
    {
        this.CurrentState = CurrentState;
    }

    public IGameState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.UIController = Controller.GetUIController();
            this.UIController.SetStoryPanelVisible(true);
            this.UIController.HideAllButton();
            this.StoryUIController = this.UIController.GetStoryUIController();

            StateStory();
        }

        if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            this.CurrentState = this.CurrentState.Next(Controller);
            StateStory();
        }

        if (!(CurrentState is IStoryState) && !(CurrentState is SomeTextState))
        {
            this.UIController.SetStoryPanelVisible(false);
            return CurrentState;
        }

        return this;
    }

    protected abstract void StateStory();
    public abstract bool IsStatableNextStory();
    public abstract IStoryState GetNextStory();
}
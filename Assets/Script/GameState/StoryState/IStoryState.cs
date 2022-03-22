using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStoryState : IGameState
{
    private IGameState CurrentState;

    protected GameController GameController;
    protected UIController UIController;
    protected StoryUIController StoryUIController;

    public IStoryState(IGameState CurrentState)
    {
        this.CurrentState = CurrentState;
    }

    /**
     * ストーリー状態
     */
    public IGameState Next(GameController Controller)
    {
        if (this.UIController == null)
        {
            this.GameController = Controller;
            this.UIController = Controller.GetUIController();
            this.UIController.SetStoryPanelVisible(true);
            this.UIController.HideAllButton();
            this.StoryUIController = this.UIController.GetStoryUIController();

            StateStory();
        }
        //ストーリー遷移
        if (Player.GetButtonDown(Player.GamePadBoolKey.A))
        {
            this.CurrentState = this.CurrentState.Next(Controller);
            StateStory();
        }
        //ストーリー終了
        if (!(CurrentState is IStoryState) && !(CurrentState is SomeTextState))
        {
            this.UIController.SetStoryPanelVisible(false);
            return CurrentState;
        }

        return this;
    }

    protected abstract void StateStory();//ストーリー遷移
    public abstract bool IsStatableNextStory();//次のストーリーへ遷移可能か
    public abstract IStoryState GetNextStory();//次のストーリー
}

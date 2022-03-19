using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class StartState : IGameState{

    public static IStoryState StoryState = new RootStoryState();

    public IGameState Next(GameController Controller)
    {
        if (StoryState.IsStatableNextStory())
        {
            return StoryState = StoryState.GetNextStory();
        }

        return new ButtonChooseState();
    }
}

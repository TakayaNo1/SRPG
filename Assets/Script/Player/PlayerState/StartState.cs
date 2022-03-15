using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class StartState : IPlayerState{

    private static IStoryState StoryState = new RootStoryState();

    public IPlayerState Next(GameController Controller)
    {
        if (StoryState.IsStatableNextStory())
        {
            return StoryState = StoryState.GetNextStory();
        }
        return new ButtonChooseState();
    }
}

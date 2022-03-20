using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class StartState : IGameState{

    public static IStoryState StoryState = new RootStoryState();

    public IGameState Next(GameController Controller)
    {
        PlayableEntity entity = Controller.GetCurrentPlayableEntity();

        if (StoryState.IsStatableNextStory())
        {
            return StoryState = StoryState.GetNextStory();
        }
        if(StoryState is StoryStateLast)
        {
            return new ResultState();
        }

        if (entity is Boss)
        {
            return new BossStartState();
        }

        int hp = Controller.GetCurrentPlayer().GetParameta(EntityParamsType.HP).Value;
        if (hp == 0)
        {
            return new SkipState();
        }

        return new ButtonChooseState();
    }
}

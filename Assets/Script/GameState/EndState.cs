using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EndState : IGameState{

    public IGameState Next(GameController Controller)
    {
        PlayableEntity entity = Controller.ShiftPlayer();

        if (StartState.StoryState.IsStatableNextStory())
        {
            return StartState.StoryState = StartState.StoryState.GetNextStory();
        }

        if (entity is Boss)
        {
            return new BossStartState();
        }

        return new StartState();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EndState : IGameState{

    /**
     * 次の順番のプレイヤー/ボスへ
     */
    public IGameState Next(GameController Controller)
    {
        Controller.ShiftPlayer();

        //if (Controller.StoryState.IsStatableNextStory())
        //{
        //    return Controller.StoryState = Controller.StoryState.GetNextStory();
        //}

        //if (entity is Boss)
        //{
        //    return new BossStartState();
        //}

        return new StartState();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class StartState : IGameState{

    /**
     * 初期状態
     */
    public IGameState Next(GameController Controller)
    {
        PlayableEntity entity = Controller.GetCurrentPlayableEntity();

        //ストーリー遷移可能ならストーリーへ
        if (Controller.StoryState.IsStatableNextStory())
        {
            return Controller.StoryState = Controller.StoryState.GetNextStory();
        }
        //ストーリーが最後ならリザルト画面へ
        if (Controller.StoryState is StoryStateLast)
        {
            return new ResultState();
        }
        //ボスのターンならボス操作へ
        if (entity is Boss)
        {
            return new BossStartState();
        }
        //プレイヤーのHPが0ならスキップへ
        int hp = Controller.GetCurrentPlayer().GetParameta(EntityParamsType.HP).Value;
        if (hp == 0)
        {
            return new SkipState();
        }
        //プレイヤー操作へ
        return new ButtonChooseState();
    }
}

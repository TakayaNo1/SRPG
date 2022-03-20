
using UnityEngine;

public class StoryState1 : IStoryState
{
    private static string[] Texts1 = new string[] { 
        "「よく来てくれた勇者よ。", "魔物討伐を依頼したい」",        //state = 0,1
        SomeTextState.ClearText+"「。。。」",                        //state = 2
        SomeTextState.ClearText+"「勇者よ頼んだぞ」",                //state = 3
        SomeTextState.ClearText+"「。。。", "わかりました。」" };    //state = 4,5
    
    private int state = 0;
    
    public StoryState1() : base(new SomeTextState(Texts1, new StartState()))
    {
    }

    protected override void StateStory()
    {
        if (state == 0)
        {
            base.StoryUIController.SetLeftImage("Texture/Yuusya");
            base.StoryUIController.SetRightImage("Texture/King");
            base.StoryUIController.SetLeftImageAlpha(0.2f);
            base.StoryUIController.SetRightImageAlpha(1.0f);
        }
        else if (state == 1)
        {
        }
        else if (state == 2)
        {
            base.StoryUIController.SetLeftImageAlpha(1.0f);
            base.StoryUIController.SetRightImageAlpha(0.2f);
        }
        else if (state == 3)
        {
            base.StoryUIController.SetLeftImageAlpha(0.2f);
            base.StoryUIController.SetRightImageAlpha(1.0f);
        }
        else if (state == 4)
        {
            base.StoryUIController.SetLeftImageAlpha(1.0f);
            base.StoryUIController.SetRightImageAlpha(0.2f);
        }
        state++;
    }

    public override bool IsStatableNextStory()
    {
        return BattleState.EnemyLevel == 2;
    }

    public override IStoryState GetNextStory()
    {
        return new StoryState2();
    }
}

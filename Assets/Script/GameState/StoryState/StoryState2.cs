
using UnityEngine;

public class StoryState2 : IStoryState
{
    private static string[] Texts2 = new string[] { 
        "「魔物を1匹討伐してくれたようだな", "よくやってくれた！」", //state = 0,1
        SomeTextState.ClearText+"「はい！」",                        //state = 2
        SomeTextState.ClearText+"「勇者よ、また頼んだぞ」",          //state = 3
        SomeTextState.ClearText+"「。。。", "わかりました。」" };    //state = 4,5
    
    private int state = 0;
    
    public StoryState2() : base(new SomeTextState(Texts2, new ButtonChooseState()))
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
        return false;
    }
    public override IStoryState GetNextStory()
    {
        return null;
    }
}

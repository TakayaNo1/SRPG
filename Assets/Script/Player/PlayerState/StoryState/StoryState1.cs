
using UnityEngine;

public class StoryState1 : IStoryState
{
    private static string[] Texts = new string[] { 
        "「よく来てくれた勇者よ。", "魔物討伐を依頼したい」",
        SomeTextState.ClearText+"「。。。」",
        SomeTextState.ClearText+"「勇者よ頼んだぞ」",
        SomeTextState.ClearText+"「。。。", "わかりました。」" };
    
    private int state = 0;
    
    public StoryState1() : base(new SomeTextState(Texts, new ButtonChooseState()))
    {
    }

    public override IPlayerState NextStory(GameController Controller)
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
        return this;
    }
}

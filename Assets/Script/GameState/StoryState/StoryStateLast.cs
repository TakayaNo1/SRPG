
using UnityEngine;

public class StoryStateLast : IStoryState
{
    private static string[] Texts = new string[] {
        "「Bossを討伐してくれたようだな", "よくやってくれた！」",                           //state = 0,1
        SomeTextState.ClearText+"「はい！」",                                               //state = 2
        SomeTextState.ClearText+"「これでこの王国は救われた！", "これでゲームクリアだ！」"  //state = 3,4
    };
    private int state = 0;
    
    public StoryStateLast() : base(new SomeTextState(Texts, new StartState()))
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

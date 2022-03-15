
using UnityEngine;

public class RootStoryState : IStoryState
{
    private static string[] Texts = new string[0];
    
    private int state = 0;
    
    public RootStoryState() : base(new SomeTextState(Texts, new ButtonChooseState()))
    {
    }

    protected override void StateStory()
    {
    }

    public override bool IsStatableNextStory()
    {
        return true;
    }

    public override IStoryState GetNextStory()
    {
        return new StoryState1();
    }
}

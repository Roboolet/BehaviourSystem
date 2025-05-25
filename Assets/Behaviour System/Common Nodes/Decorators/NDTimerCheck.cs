using UnityEngine;

public class NDTimerCheck : NDecorator
{
    private readonly string timerBlackboardKey;
    private readonly float requiredTimeElapsed;
    
    public NDTimerCheck(INode _child, string _timerBlackboardKey, float _requiredTimeElapsed) : base(_child)
    {
        timerBlackboardKey = _timerBlackboardKey;
        requiredTimeElapsed = _requiredTimeElapsed;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        float timer = Mathf.NegativeInfinity;
        if (bb.TryGet(timerBlackboardKey, out float value))
        {
            timer = value;
        }

        if (timer + requiredTimeElapsed < Time.time)
        {
            return child.Execute(bb);
        }
        else
        {
            return NodeReturnState.FAILED;
        }
    }
}

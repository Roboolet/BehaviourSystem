using UnityEngine;

public class NTimerSet : ANode
{
    private readonly string timerBlackboardKey;
    
    public NTimerSet(string _timerBlackboardKey)
    {
        timerBlackboardKey = _timerBlackboardKey;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        bb.Set(timerBlackboardKey, Time.time);
        return NodeReturnState.SUCCESS;
    }
}

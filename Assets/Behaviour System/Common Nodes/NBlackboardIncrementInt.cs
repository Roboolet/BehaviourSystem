using UnityEngine;

public class NBlackboardIncrementInt: ANode
{
    private readonly string blackboardKey;
    private readonly int incrementValue;
    
    public NBlackboardIncrementInt(string _blackboardKey, int _incrementValue = 1)
    {
        blackboardKey = _blackboardKey;
        incrementValue = _incrementValue;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        int oldVal = bb.Get<int>(blackboardKey);
        bb.Set(blackboardKey, oldVal + incrementValue);
        return NodeReturnState.SUCCESS;

    }
}

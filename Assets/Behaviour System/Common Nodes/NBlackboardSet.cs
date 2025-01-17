using UnityEngine;

public class NBlackboardSet : ANode
{
    private readonly string blackboardKey;
    private readonly object value;
    
    public NBlackboardSet(string _blackboardKey, object _value)
    {
        blackboardKey = _blackboardKey;
        value = _value;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        bb.Set(blackboardKey, value);
        return NodeReturnState.SUCCESS;

    }
}

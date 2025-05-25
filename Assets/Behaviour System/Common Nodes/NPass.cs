using UnityEngine;

public class NPass : ANode
{
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        return NodeReturnState.SUCCESS;
    }
}

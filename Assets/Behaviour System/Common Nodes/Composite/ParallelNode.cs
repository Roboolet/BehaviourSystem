using UnityEngine;

public class ParallelNode : CompositeNode
{
    public ParallelNode(INode[] _children) : base(_children)
    {
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        return base.OnExecute(bb);
    }
}

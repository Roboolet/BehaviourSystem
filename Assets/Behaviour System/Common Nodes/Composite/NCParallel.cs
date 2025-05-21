using UnityEngine;

public class NCParallel : NComposite
{
    public NCParallel(params INode[] _children) : base(_children)
    {
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        return base.OnExecute(bb);
    }
}

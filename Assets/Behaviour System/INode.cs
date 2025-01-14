using UnityEngine;

public interface INode
{
    public NodeReturnState Execute(Blackboard bb);
}

public abstract class ANode : INode
{
    public NodeReturnState Execute(Blackboard bb)
    {
        if (bb == null)
        {
            Debug.LogError("This node does not have a Blackboard reference");
            return NodeReturnState.ERROR;
        }
        else
        {
            bb.Set("current_node", this);
        }
        return OnExecute(bb);
    }

    protected virtual NodeReturnState OnExecute(Blackboard bb)
    {
        Debug.LogWarning("This node has no implemented behaviour");
        return NodeReturnState.ERROR;
    }
}

public abstract class DecoratorNode : ANode
{
    public readonly INode child;

    public DecoratorNode(INode _child)
    {
        child = _child;
    }
    
}

public abstract class CompositeNode : ANode
{
    public readonly INode[] children;
    protected int index;
    
    public CompositeNode(INode[] _children)
    {
        children = _children;
    }
}

public enum NodeReturnState
{
    ERROR,
    FAILED,
    SUCCESS,
    RUNNING
}

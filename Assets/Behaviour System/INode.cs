using Unity.VisualScripting;
using UnityEngine;

public interface INode
{
    public NodeReturnState Execute(Blackboard bb);
}

public abstract class ANode : INode
{
    private readonly string nodeName;

    protected ANode()
    {
        nodeName = this.GetType().Name;
    }
    
    public NodeReturnState Execute(Blackboard bb)
    {
        if (bb == null)
        {
            Debug.LogError(nodeName + " does not have a Blackboard reference");
            return NodeReturnState.ERROR;
        }
        else
        {
            string newPath;
            if (bb.TryGet("common_current_node", out string oldPath))
            {
                newPath = oldPath + " -> " + nodeName;
            }
            else
            {
                newPath = nodeName;
            }
            
            bb.Set("common_current_node", newPath);
        }
        return OnExecute(bb);
    }

    protected virtual NodeReturnState OnExecute(Blackboard bb)
    {
        Debug.LogWarning("This node has no implemented behaviour");
        return NodeReturnState.ERROR;
    }
}

public abstract class NDecorator : ANode
{
    public readonly INode child;

    public NDecorator(INode _child)
    {
        child = _child;
    }
    
}

public abstract class NComposite : ANode
{
    public readonly INode[] children;
    protected int index;
    
    public NComposite(INode[] _children)
    {
        children = _children;
    }
}

public enum NodeReturnState
{
    // error is used exclusively when something has gone horribly wrong
    ERROR,
    FAILED,
    SUCCESS,
    RUNNING
}

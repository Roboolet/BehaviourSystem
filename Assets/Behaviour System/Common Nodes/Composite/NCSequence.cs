using System;
using UnityEngine;

public class NCSequence : NComposite
{
    public NCSequence(params INode[] _children) : base(_children)
    {
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        NodeReturnState ret = NodeReturnState.ERROR;
        try
        {
            ret = children[index].Execute(bb);
        }
        catch (Exception e)
        {
            Debug.LogError("Child count: "+children.Length);
            Debug.LogError("Index '" + index +"': " + e.Message);
        }

        switch (ret)
        {
            default: return ret;
            
            case NodeReturnState.SUCCESS:
                index++;
                if (index >= children.Length)
                {
                    index = 0;
                    return NodeReturnState.SUCCESS;
                }
                // if not yet past every node, return running
                return NodeReturnState.RUNNING;
            
            // if errored or failed, reset index
            case NodeReturnState.ERROR:
            case NodeReturnState.FAILED:
                index = 0;
                return ret;
                
        }
    }
}

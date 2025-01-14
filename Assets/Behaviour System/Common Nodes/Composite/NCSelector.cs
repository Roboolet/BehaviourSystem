using UnityEngine;

public class NCSelector : NComposite
{
    public NCSelector(INode[] _children) : base(_children)
    {
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        NodeReturnState ret = children[index].Execute(bb);

        switch (ret)
        {
            default: return ret;
            
            case NodeReturnState.SUCCESS:
                index = 0;
                return NodeReturnState.SUCCESS;
            
            case NodeReturnState.ERROR:
                index = 0;
                return NodeReturnState.ERROR;
            
            case NodeReturnState.FAILED:
                index++;
                index %= children.Length;
                return ret;
                
        }
    }
}

using UnityEngine;

public class InvertNode : DecoratorNode
{
    public InvertNode(INode _child) : base(_child)
    {
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        NodeReturnState r = child.Execute(bb);
        switch (r)
        {
            case NodeReturnState.FAILED:
                r = NodeReturnState.SUCCESS;
                break;
            case NodeReturnState.SUCCESS:
                r = NodeReturnState.FAILED;
                break;
        }

        return r;
    }
}

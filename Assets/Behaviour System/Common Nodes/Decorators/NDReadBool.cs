using UnityEngine;

public class NDReadBool: NDecorator
{
    private readonly string blackboardKey;
    private readonly bool invert;
    
    public NDReadBool(INode _child, string _blackboardKey, bool _invert = false) : base(_child)
    {
        blackboardKey = _blackboardKey;
        invert = _invert;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(blackboardKey, out bool b))
        {
            if(invert){b = !b;}

            if (b)
            {
                return child.Execute(bb);
            }
            return NodeReturnState.FAILED;
        }

        return NodeReturnState.ERROR;
    }
}

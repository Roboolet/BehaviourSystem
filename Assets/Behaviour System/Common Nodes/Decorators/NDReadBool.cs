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
        bool b = bb.Get<bool>(blackboardKey);

        if(invert){b = !b;}

        if (b)
        {
            return child.Execute(bb);
        }
        else
        {
            return NodeReturnState.FAILED;
        }
    }
}

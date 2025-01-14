using System;
using UnityEngine;

public class NDCondition : NDecorator
{
    private readonly Func<bool> condition;
    public NDCondition(INode _child, Func<bool> _condition) : base(_child)
    {
        condition = _condition;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (condition.Invoke())
        {
            return child.Execute(bb);
        }
        else
        {
            return NodeReturnState.FAILED;
        }
    }
}

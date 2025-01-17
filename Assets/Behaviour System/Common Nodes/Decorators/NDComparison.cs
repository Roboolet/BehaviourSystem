using UnityEngine;
using System;

public enum Comparator {EQUALS, GREATER}

public class NDComparison<T> : NDecorator where T : IComparable
{

    private readonly Comparator comparator;
    private readonly bool invert;
    private readonly string blackboardKey;
    private readonly T value;

    public NDComparison(INode _child, string _blackboardKey, Comparator _comparator, T _value, bool _invert = false) : base(_child)
    {
        comparator = _comparator;
        invert = _invert;
        blackboardKey = _blackboardKey;
        value = _value;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(blackboardKey, out T bbVal))
        {
            int comp = bbVal.CompareTo(value);

            // excuse the mess
            bool compBool = ((comparator == Comparator.EQUALS && comp == 0) ||
                             (comparator == Comparator.GREATER && comp > 0));
            if (invert) { compBool = !compBool; }
            
            if (compBool)
            {
                return child.Execute(bb);
            }
            else
            {
                return NodeReturnState.FAILED;
            }
        }

        return NodeReturnState.ERROR;
    }
}

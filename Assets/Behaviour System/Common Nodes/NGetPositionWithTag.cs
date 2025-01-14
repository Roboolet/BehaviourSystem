using UnityEngine;

public class NGetPositionWithTag : ANode
{
    private readonly string targetTag, blackboardOutput;

    public NGetPositionWithTag(string _targetTag, string _blackboardOutput)
    {
        targetTag = _targetTag;
        blackboardOutput = _blackboardOutput;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        GameObject go = GameObject.FindGameObjectWithTag(targetTag);
        if (go != null)
        {
            bb.Set(blackboardOutput, go.transform.position);
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.FAILED;
    }
}

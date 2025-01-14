using UnityEngine;

public class NGetPositionWithTag : ANode
{
    private readonly string targetTag, blackboardKey;

    public NGetPositionWithTag(string _targetTag, string _blackboardKey)
    {
        targetTag = _targetTag;
        blackboardKey = _blackboardKey;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        GameObject go = GameObject.FindGameObjectWithTag(targetTag);
        if (go != null)
        {
            bb.Set(blackboardKey, go.transform.position);
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.FAILED;
    }
}

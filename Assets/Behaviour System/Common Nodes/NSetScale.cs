using UnityEngine;

public class NSetScale : ANode
{
    private readonly string bbScale;
    private readonly float scalingFactor, minimumSize;
    
    public NSetScale(string _scaleBlackboardKey,
        float _minimumSize,
        float _scalingFactor = 1)
    {
        bbScale = _scaleBlackboardKey;
        scalingFactor = _scalingFactor;
        minimumSize = _minimumSize;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(bbScale, out float size))
        {
            float s = minimumSize + size * scalingFactor;
            bb.Get<GameObject>(CommonBB.AGENT_GAMEOBJECT).transform.localScale =
                new Vector3(s, s, s);
            return NodeReturnState.SUCCESS;
        }
        else return NodeReturnState.ERROR;
    }
}

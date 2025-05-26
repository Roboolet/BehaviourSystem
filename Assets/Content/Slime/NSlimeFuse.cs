using UnityEngine;

public class NSlimeFuse : ANode
{
    private readonly string bbTarget;
    private readonly string bbSize;
    
    public NSlimeFuse(string _targetBlackboardKey, string _slimeSizeBlackboardKey)
    {
        bbTarget = _targetBlackboardKey;
        bbSize = _slimeSizeBlackboardKey;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        Agent target = bb.Get<GameObject>(bbTarget).GetComponent<Agent>();
        Agent self = bb.Get<Agent>(CommonBB.AGENT);
        
        float selfSize = bb.Get<float>(bbSize);
        float targetSize = target.blackboard.Get<float>(bbSize);

        if (self.transform.GetSiblingIndex() < target.transform.GetSiblingIndex())
        {
            bb.Set(bbSize, selfSize + targetSize);
            target.Destroy();
        }
        else
        {
            target.blackboard.Set(bbSize, selfSize + targetSize);
            self.Destroy();
        }
        
        return NodeReturnState.SUCCESS;
    }
}

using UnityEngine;

public class NSlimeFuse : ANode
{
    private readonly string bbTarget;
    private readonly string bbSize;
    
    public NSlimeFuse(string _targetBlackboardKey, string _slimeSizeBlackboardKey)
    {
        bbTarget = _targetBlackboardKey;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        Agent target = bb.Get<GameObject>(bbTarget).GetComponent<Agent>();
        Agent self = bb.Get<Agent>(CommonBB.AGENT);

        if (self.transform.GetSiblingIndex() < target.transform.GetSiblingIndex())
        {
            float selfSize = bb.Get<float>(bbSize);
            float targetSize = target.blackboard.Get<float>(bbSize);
            
            bb.Set(bbSize, selfSize + targetSize);
        }
        else
        {
            //Object.Destroy(self);
        }
        
        return NodeReturnState.SUCCESS;
    }
}

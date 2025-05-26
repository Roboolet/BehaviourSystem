using UnityEngine;
using UnityEngine.AI;

public class NSlimeSetSpeed : ANode
{
    private readonly string bbSize;
    private readonly float baseSpeed;
    
    public NSlimeSetSpeed(string _sizeBlackboardKey, float _baseSpeed)
    {
        bbSize = _sizeBlackboardKey;
        baseSpeed = _baseSpeed;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(bbSize, out float size))
        {
            NavMeshAgent a = bb.Get<Agent>(CommonBB.AGENT).GetComponent<NavMeshAgent>();
            float newSpeed = baseSpeed / size;
            a.speed = newSpeed;
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.ERROR;

    }
}

using UnityEngine;
using UnityEngine.AI;

public class NStopMoving : ANode
{
    private readonly bool noFail;
    
    public NStopMoving(bool _noFail = false)
    {
        noFail = _noFail;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        NavMeshAgent a = bb.Get<Agent>(CommonBB.AGENT).GetComponent<NavMeshAgent>();
        if (a == null) return NodeReturnState.ERROR;

        if (!a.isStopped)
        {
            a.isStopped = true;
            return NodeReturnState.SUCCESS;
        }
        else
        {
            if (noFail) return NodeReturnState.SUCCESS;
            return NodeReturnState.FAILED;
        }
    }
}

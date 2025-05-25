using UnityEngine;
using UnityEngine.AI;

public class NStopMoving : ANode
{
    public NStopMoving()
    {
        
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
            return NodeReturnState.FAILED;
        }
    }
}

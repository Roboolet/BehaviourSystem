using UnityEngine;
using UnityEngine.AI;

public class NMoveTowards : ANode
{
    private readonly string destinationBlackboardName;
    private NavMeshAgent navAgent;

    public NMoveTowards(string _destinationBlackboardName)
    {
        destinationBlackboardName = _destinationBlackboardName;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        // get agent
        if (navAgent == null)
        {
            if (bb.TryGet("common_agent", out Agent agent))
            {
                navAgent = agent.GetComponent<NavMeshAgent>();
            }
            else
            {
                Debug.LogWarning("Could not find agent in blackboard");
            }
        }

        // set destination
        if (bb.TryGet(destinationBlackboardName, out Vector3 newDestination))
        {
            navAgent.SetDestination(newDestination);
            return NodeReturnState.SUCCESS;
        }
        else
        {
            Debug.LogWarning("Could not find MoveTowards destination in blackboard using name: "+destinationBlackboardName);
        }
        
        return NodeReturnState.FAILED;
    }
}

using UnityEngine;
using UnityEngine.AI;

public enum PositionReadMode {VECTOR3, GAME_OBJECT}

public class NMoveTowards : ANode
{
    
    private readonly string destinationBlackboardName;
    private readonly PositionReadMode moveMode;
    private NavMeshAgent navAgent;

    public NMoveTowards(string _destinationBlackboardName, PositionReadMode _moveMode)
    {
        destinationBlackboardName = _destinationBlackboardName;
        moveMode = _moveMode;
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
        }

        // set destination
        switch (moveMode)
        {
            // go to position
            default:
                if (bb.TryGet(destinationBlackboardName, out Vector3 newDestination))
                {
                    navAgent.SetDestination(newDestination);
                    return NodeReturnState.SUCCESS;
                }
                else return NodeReturnState.FAILED;
            
            // go to gameobject
            case PositionReadMode.GAME_OBJECT:
                if (bb.TryGet(destinationBlackboardName, out GameObject targetGameObject))
                {
                    navAgent.SetDestination(targetGameObject.transform.position);
                    return NodeReturnState.SUCCESS;
                }
                else return NodeReturnState.FAILED;
        }
    }
}

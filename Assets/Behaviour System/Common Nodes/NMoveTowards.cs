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
            if (bb.TryGet(CommonBB.AGENT, out Agent agent))
            {
                navAgent = agent.GetComponent<NavMeshAgent>();
            }
        }

        // set destination
        Vector3 destination = Vector3.zero;
        switch (moveMode)
        {
            case PositionReadMode.VECTOR3:
                destination = bb.Get<Vector3>(destinationBlackboardName);
                break;
            case PositionReadMode.GAME_OBJECT:
                GameObject go = bb.Get<GameObject>(destinationBlackboardName);
                if (go != null)
                {
                    destination = go.transform.position;
                }
                else return NodeReturnState.FAILED;

                break;
        }

        navAgent.isStopped = false;
        navAgent.SetDestination(destination);
        return NodeReturnState.SUCCESS;
    }
}

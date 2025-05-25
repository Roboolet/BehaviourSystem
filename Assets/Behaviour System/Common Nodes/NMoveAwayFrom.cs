using UnityEngine;
using UnityEngine.AI;

public class NMoveAwayFrom : ANode
{
    private readonly string bbTarget, bbOrigin;
    private readonly PositionReadMode posMode;
    private readonly NavMeshQueryFilter navFilter;
    private readonly float idealDistance;

    public NMoveAwayFrom(string _targetBlackboardKey,
        NavMeshQueryFilter _navFilter,
        float _idealDistance,
        string _originBlackboardKey = CommonBB.AGENT_GAMEOBJECT,
        PositionReadMode _posMode = PositionReadMode.GAME_OBJECT)
    {
        bbTarget = _targetBlackboardKey;
        bbOrigin = _originBlackboardKey;
        posMode = _posMode;
        navFilter = _navFilter;
        idealDistance = _idealDistance;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        // get navAgent
        NavMeshAgent navAgent = bb.Get<Agent>(CommonBB.AGENT).GetComponent<NavMeshAgent>();
        if (navAgent == null) return NodeReturnState.ERROR;

        // get position of target
        Vector3 targetPosition = Vector3.zero;
        Vector3 originPosition = Vector3.zero;
        switch (posMode)
        {
            case PositionReadMode.VECTOR3:
                targetPosition = bb.Get<Vector3>(bbTarget);
                originPosition = bb.Get<Vector3>(bbOrigin);
                break;
            case PositionReadMode.GAME_OBJECT:
                targetPosition = bb.Get<GameObject>(bbTarget).transform.position;
                originPosition = bb.Get<GameObject>(bbOrigin).transform.position; 
                break;
        }
        
        // get direction from target to origin
        Vector3 diff = originPosition - targetPosition;
        float mag = idealDistance - diff.magnitude;
        Vector3 destination = originPosition + diff.normalized*mag;
        
        // check if destination is valid, if not, find closest valid point
        /*NavMesh.SamplePosition(destination, out NavMeshHit hit, 1f, navFilter);
        if (!hit.hit)
        {
            NavMesh.FindClosestEdge(destination, out NavMeshHit newPosHit, navFilter);
            destination = newPosHit.position;
        }*/
        
        navAgent.isStopped = false;
        navAgent.SetDestination(destination);
        return NodeReturnState.SUCCESS;
    }
}

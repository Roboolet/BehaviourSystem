using UnityEngine;

public class NGetClosestPatrolPoint : ANode
{
    private readonly string patrolRouteBlackboardKey;
    private readonly string outputBlackboardKey;
    private readonly string agentBlackboardKey;

    public NGetClosestPatrolPoint(string _outputBlackboardKey
        , string _patrolRouteBlackboardKey = "patrol_route"
        , string _agentBlackboardKey = CommonBB.AGENT_GAMEOBJECT)
    {
        outputBlackboardKey = _outputBlackboardKey;
        patrolRouteBlackboardKey = _patrolRouteBlackboardKey;
        agentBlackboardKey = _agentBlackboardKey;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(patrolRouteBlackboardKey, out PatrolRoute route))
        {
            Vector3 pos = bb.Get<GameObject>(agentBlackboardKey).transform.position;
            bb.Set(outputBlackboardKey, route.GetClosestWaypoint(pos));
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.FAILED;
    }
}

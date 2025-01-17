using UnityEngine;

public class NGetNextPatrolPoint : ANode
{
    private readonly string patrolRouteBlackboardKey;
    private readonly string currentWaypointBlackboardKey;
    private readonly string outputBlackboardKey;
    private readonly string agentBlackboardKey;

    public NGetNextPatrolPoint(string _currentWaypointBlackboardKey, string _outputBlackboardKey
        , string _patrolRouteBlackboardKey = "patrol_route"
        , string _agentBlackboardKey = "common_agent_gameobject")
    {
        outputBlackboardKey = _outputBlackboardKey;
        patrolRouteBlackboardKey = _patrolRouteBlackboardKey;
        agentBlackboardKey = _agentBlackboardKey;
        currentWaypointBlackboardKey = _currentWaypointBlackboardKey;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(patrolRouteBlackboardKey, out PatrolRoute route))
        {
            Vector3 pos = bb.Get<GameObject>(agentBlackboardKey).transform.position;
            bb.Set(outputBlackboardKey, route.GetNextWaypoint(
                route.GetIndexOfWaypoint(
                    bb.Get<GameObject>(currentWaypointBlackboardKey).transform)));
            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.FAILED;
    }
}

using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class NGetDistanceTo : ANode
{
    private readonly PositionReadMode readMode;
    private readonly string target;
    private readonly string output;
    private readonly string origin;
    
    public NGetDistanceTo(string _targetBlackboardKey, string _outputBlackboardKey,
        PositionReadMode _readMode, string _originBlackboardKey = "common_agent_gameobject") : base()
    {
        target = _targetBlackboardKey;
        origin = _originBlackboardKey;
        output = _outputBlackboardKey;
        readMode = _readMode;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        UnityEngine.Vector3 selfPos = bb.Get<GameObject>(origin).transform.position;
        switch (readMode)
        {
            default:
                if (bb.TryGet(target, out UnityEngine.Vector3 vec))
                {
                    bb.Set(output, (vec - selfPos).magnitude);
                    return NodeReturnState.SUCCESS;
                }
                return NodeReturnState.ERROR;
            
            case PositionReadMode.GAME_OBJECT:
                if (bb.TryGet(target, out GameObject go))
                {
                    bb.Set(output, (go.transform.position - selfPos).magnitude);
                    return NodeReturnState.SUCCESS;
                }
                return NodeReturnState.ERROR;
        }
    }
}

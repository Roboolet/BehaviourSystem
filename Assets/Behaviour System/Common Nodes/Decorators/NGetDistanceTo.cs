using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class NGetDistanceTo : ANode
{
    private readonly PositionReadMode readMode;
    private readonly string target;
    private readonly string output;
    
    public NGetDistanceTo(string _targetBlackboardKey, string _outputBlackboardKey,
        PositionReadMode _readMode) : base()
    {
        target = _targetBlackboardKey;
        output = _outputBlackboardKey;
        readMode = _readMode;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        UnityEngine.Vector3 selfPos = bb.Get<GameObject>("common_agent_gameobject").transform.position;
        switch (readMode)
        {
            case PositionReadMode.VECTOR3:
                if (bb.TryGet(target, out UnityEngine.Vector3 vec))
                {
                    bb.Set(output, (vec - selfPos).magnitude);
                }
                return NodeReturnState.FAILED;
            
            case PositionReadMode.GAME_OBJECT:
                if (bb.TryGet(target, out GameObject go))
                {
                    bb.Set(output, (go.transform.position - selfPos).magnitude);
                }
                return NodeReturnState.FAILED;
        }

        return NodeReturnState.ERROR;
    }
}

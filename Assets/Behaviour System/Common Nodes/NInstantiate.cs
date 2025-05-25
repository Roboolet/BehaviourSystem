using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class NInstantiate : ANode
{
    protected readonly GameObject prefab;
    protected readonly string origin;
    protected readonly PositionReadMode posMode;
    protected readonly InstantiateRotationMode rotMode;

    public NInstantiate(GameObject _prefab,
        string _originBlackboardKey = CommonBB.AGENT_GAMEOBJECT,
        PositionReadMode _posMode = PositionReadMode.GAME_OBJECT,
        InstantiateRotationMode _rotMode = InstantiateRotationMode.IDENTITY)
    {
        prefab = _prefab;
        origin = _originBlackboardKey;
        posMode = _posMode;
        rotMode = _rotMode;
    }
        
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        Agent agent = bb.Get<Agent>(CommonBB.AGENT);
        
        // get position
        Vector3 pos = Vector3.zero;
        if (!String.IsNullOrEmpty(origin))
        {
            switch (posMode)
            {
                case PositionReadMode.VECTOR3:
                    if (bb.TryGet(origin, out UnityEngine.Vector3 vec))
                    {
                        pos = vec;
                    }
                    else return NodeReturnState.ERROR;
                    break;
                
                case PositionReadMode.GAME_OBJECT:
                    if (bb.TryGet(origin, out GameObject go))
                    {
                        pos = go.transform.position;
                    }
                    else return NodeReturnState.ERROR;
                    break;
            }
        }

        // get rotation
        Quaternion rot = Quaternion.identity;
        switch (rotMode)
        {
            case InstantiateRotationMode.AGENT_INHERIT:
                rot = agent.transform.rotation;
                break;
        }
        
        // instantiate
        GameObject obj = Object.Instantiate(prefab, pos, rot);
        ModifyInstantiatedObject(bb, obj);
        return NodeReturnState.SUCCESS;
    }
    
    // modify this in overrides of this node
    protected virtual void ModifyInstantiatedObject(Blackboard _bb, GameObject _gameObject){}
}

public enum InstantiateRotationMode
{
    IDENTITY,
    AGENT_INHERIT /*,
    ORIGIN_INHERIT,
    AGENT_FACING_AWAY,
    AGENT_FACING_TOWARDS,*/
}

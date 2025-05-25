using System.Collections.Generic;
using UnityEngine;

public class NGetClosestGameObjectInList : ANode 
{
    private readonly string bbList, bbOrigin, bbOutput;
    private readonly PositionReadMode posMode;
    
    public NGetClosestGameObjectInList(string _listBlackboardKey,
        string _outputBlackboardKey,
        string _originBlackboardKey = CommonBB.AGENT_GAMEOBJECT,
        PositionReadMode _positionReadMode = PositionReadMode.GAME_OBJECT)
    {
        bbList = _listBlackboardKey;
        bbOrigin = _originBlackboardKey;
        posMode = _positionReadMode;
        bbOutput = _outputBlackboardKey;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        List<GameObject> gos = bb.Get<List<GameObject>>(bbList);
        Vector3 origin = Vector3.zero;
        switch (posMode)
        {
            case PositionReadMode.VECTOR3: origin = bb.Get<Vector3>(bbOrigin); break;
            case PositionReadMode.GAME_OBJECT: origin = 
                bb.Get<GameObject>(bbOrigin).transform.position; break;
        }

        GameObject closest = null;
        float closestDist = Mathf.Infinity;
        for (int i = 0; i < gos.Count; i++)
        {
            GameObject go = gos[i];
            float dist = Vector3.Distance(go.transform.position, origin);
            if (dist < closestDist)
            {
                closest = go;
                closestDist = dist;
            }
        }

        if (closest == null) return NodeReturnState.ERROR;
        
        bb.Set(bbOutput, closest);
        return NodeReturnState.SUCCESS;
    }
}

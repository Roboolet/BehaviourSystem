using UnityEngine;

public class NGetLineOfSight : ANode
{
    private readonly string targetBlackboardKey;
    private readonly string originBlackboardKey;
    private readonly string outputBlackboardKey;
    private readonly LayerMask layerMask;

    /// <summary>
    /// Executes the child if there are no obstructions with raycast.
    /// </summary>
    /// <param name="_targetBlackboardKey"></param>
    /// <param name="_layerMask"></param>
    /// <param name="_originBlackboardKey"></param>
    public NGetLineOfSight(string _targetBlackboardKey,
        string _outputBlackboardKey,
        LayerMask _layerMask,
        string _originBlackboardKey = CommonBB.AGENT_GAMEOBJECT)
    {
        targetBlackboardKey = _targetBlackboardKey;
        originBlackboardKey = _originBlackboardKey;
        outputBlackboardKey = _outputBlackboardKey;
        layerMask = _layerMask;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        Vector3 origin = Vector3.zero;
        Vector3 target = Vector3.zero;
        
        if (bb.TryGet(originBlackboardKey, out GameObject originObj))
        {
            origin = originObj.transform.position;
            if (bb.TryGet(targetBlackboardKey, out GameObject targetObj))
            {
                target = targetObj.transform.position;
                
                Ray ray = new Ray(origin, (target-origin).normalized);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.transform == targetObj.transform)
                    {
                        bb.Set(outputBlackboardKey, true);
                        return NodeReturnState.SUCCESS;
                    }
                }
                bb.Set(outputBlackboardKey, false);
                return NodeReturnState.SUCCESS;
            }
        }

        return NodeReturnState.FAILED;
    }
}

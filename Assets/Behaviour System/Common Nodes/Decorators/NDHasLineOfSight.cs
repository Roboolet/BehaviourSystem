using UnityEngine;

public class NDHasLineOfSight : NDecorator
{
    private readonly string targetBlackboardKey;
    private readonly string originBlackboardKey;
    private readonly LayerMask layerMask;

    /// <summary>
    /// Executes the child if there are no obstructions with raycast.
    /// </summary>
    /// <param name="_child"></param>
    /// <param name="_targetBlackboardKey"></param>
    /// <param name="_layerMask"></param>
    /// <param name="_originBlackboardKey"></param>
    public NDHasLineOfSight(INode _child, string _targetBlackboardKey, LayerMask _layerMask, string _originBlackboardKey = "common_agent_gameobject") : base(_child)
    {
        targetBlackboardKey = _targetBlackboardKey;
        originBlackboardKey = _originBlackboardKey;
        layerMask = _layerMask;
    }
    public NDHasLineOfSight(INode _child, string _targetBlackboardKey, string _originBlackboardKey = "common_agent_gameobject") : base(_child)
    {
        targetBlackboardKey = _targetBlackboardKey;
        originBlackboardKey = _originBlackboardKey;
        layerMask = ~0;
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
                        return child.Execute(bb);
                    }
                }
                return NodeReturnState.FAILED;
            }
        }

        return NodeReturnState.ERROR;
    }
}

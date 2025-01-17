using UnityEngine;

public class NAttackPlayer : ANode
{
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        Debug.Log("Punch!");
        return NodeReturnState.SUCCESS;
    }
}

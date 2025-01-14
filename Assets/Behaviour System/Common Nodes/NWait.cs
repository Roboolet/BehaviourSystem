using UnityEngine;

public class NWait : ANode
{
    private readonly float waitSeconds;
    private float startTimestamp = Mathf.NegativeInfinity;
    
    public NWait(float _waitSeconds)
    {
        waitSeconds = _waitSeconds;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (startTimestamp + waitSeconds <= Time.time)
        {
            bool justFinished = startTimestamp > 0;
            startTimestamp = Time.time;

            if (justFinished)
            {
                return NodeReturnState.SUCCESS;
            }
        }

        return NodeReturnState.RUNNING;
    }
}

using UnityEngine;

public class NPrintBlackboard : ANode
{
    private readonly string blackboardKey;
    private readonly PrintMode mode;

    public NPrintBlackboard(PrintMode _mode, string _blackboardKey)
    {
        blackboardKey = _blackboardKey;
        mode = _mode;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(blackboardKey, out string message))
        {

            switch (mode)
            {
                case PrintMode.LOG:
                    Debug.Log(message);
                    break;

                case PrintMode.LOG_WARNING:
                    Debug.LogWarning(message);
                    break;

                case PrintMode.LOG_ERROR:
                    Debug.LogError(message);
                    break;
            }

            return NodeReturnState.SUCCESS;
        }

        return NodeReturnState.FAILED;
    }
}

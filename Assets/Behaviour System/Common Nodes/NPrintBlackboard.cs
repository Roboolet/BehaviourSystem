using UnityEngine;

public class NPrintBlackboard : ANode
{
    private readonly string blackboardKey;
    private readonly PrintMode mode;
    private readonly string prefix;
    
    public NPrintBlackboard(string _blackboardKey, PrintMode _mode = PrintMode.LOG)
    {
        blackboardKey = _blackboardKey;
        mode = _mode;

        prefix = "Blackboard key: " + blackboardKey + " = ";
    }
    public NPrintBlackboard(string _blackboardKey, PrintMode _mode, string _prefix)
    {
        blackboardKey = _blackboardKey;
        mode = _mode;
        prefix = _prefix;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        if (bb.TryGet(blackboardKey, out object obj))
        {
            string message = prefix + obj.ToString();
            
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

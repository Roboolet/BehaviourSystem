using UnityEngine;

public class PrintNode : ANode
{
    private readonly string message;
    private readonly PrintMode mode;

    public PrintNode(PrintMode _mode, string _message)
    {
        message = _message;
        mode = _mode;
    }
    
    protected override NodeReturnState OnExecute(Blackboard bb)
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
    
    public enum PrintMode{LOG, LOG_WARNING, LOG_ERROR}
}

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Agent : MonoBehaviour
{
    [SerializeField] private BehaviourBlueprint behaviour;
    public bool active = true;
    public string groupTag;
    [SerializeField, Range(0.5f,30)] private float ticksPerSecond = 20;

    [Header("Debugging")] 
    [SerializeField] private bool logCurrentNode;
    
    public Blackboard blackboard;
    
    private INode root;
    // private IEvaluator[] evaluators;
    private float lastTickTime;
    private int tickCounter;

    private void Awake()
    {
        root = behaviour.BuildTree();
        
        blackboard = new Blackboard();
        blackboard.Set(CommonBB.AGENT, this);
        blackboard.Set(CommonBB.AGENT_GAMEOBJECT, gameObject);
        Blackboard.Global.ListAdd(CommonBB.AGENTS_LIST, this);

        for (int i = 0; i < behaviour.blackboardValues.Length; i++)
        {
            BlackboardInitialValue bbVal = behaviour.blackboardValues[i];
            switch (bbVal.type)
            {
                case BlackboardValueType.INT: blackboard.Set(bbVal.key, bbVal._int); break;
                case BlackboardValueType.FLOAT: blackboard.Set(bbVal.key, bbVal._float); break;
                case BlackboardValueType.STRING: blackboard.Set(bbVal.key, bbVal._string); break;
                case BlackboardValueType.VECTOR3: blackboard.Set(bbVal.key, bbVal._vector3); break;
            }
        }
    }

    public string GetNodeLog()
    {
        // to change the formatting, see INode.cs
        return blackboard.Get<string>(CommonBB.CURRENT_NODE);
    }

    private void Update()
    {
        if (active && lastTickTime + (1 / ticksPerSecond) < Time.time)
        {
            // set common variables in the blackboard
            float tickDelta = Time.time - lastTickTime;
            lastTickTime = Time.time;
            tickCounter++;
            blackboard.Set(CommonBB.TICK_DELTA, tickDelta);
            blackboard.Set(CommonBB.TICK_TOTAL, tickCounter);
            
            // every implementation of ANode appends their node name to this, 
            // giving the "path" of the current node when the tick finishes.
            // it is necessary to clear this every tick beforehand.
            blackboard.Set(CommonBB.CURRENT_NODE, "");

            // run the root, thereby stepping forward in the tree
            NodeReturnState rootReturn = root.Execute(blackboard);
            if (logCurrentNode)
            {
                Debug.Log(GetNodeLog());
            }
            
            if (rootReturn == NodeReturnState.ERROR)
            {
                Debug.LogError("Behaviour tree of " + transform.name + 
                               " returns ERROR, stopping execution");
                active = false;
            }
        }
    }
}

// has commonly used blackboard values
public static class CommonBB
{
    public const string 
        CURRENT_NODE = "common_current_node", 
        TICK_DELTA = "common_tick_delta",
        TICK_TOTAL = "common_tick_total",
        AGENT = "common_agent",
        AGENT_GAMEOBJECT = "common_agent_gameobject",
        AGENTS_LIST = "common_agents_list";
}

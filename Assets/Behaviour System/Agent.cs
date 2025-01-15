using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public bool active = true;
    
    [SerializeField] private BehaviourBlueprint behaviour;
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
        blackboard.Set("common_agent", this);
    }

    private void Update()
    {
        if (active && lastTickTime + (1 / ticksPerSecond) < Time.time)
        {
            // set common variables in the blackboard
            float tickDelta = Time.time - lastTickTime;
            lastTickTime = Time.time;
            tickCounter++;
            blackboard.Set("common_tick_delta", tickDelta);
            blackboard.Set("common_tick_total", tickCounter);
            
            // every implementation of ANode appends their node name to this, 
            // giving the "path" of the current node when the tick finishes.
            // it is necessary to clear this every tick beforehand.
            blackboard.Set("common_current_node", "");

            // run the root, thereby stepping forward in the tree
            if (root.Execute(blackboard) == NodeReturnState.ERROR)
            {
                Debug.LogError("Behaviour tree of " + transform.name + 
                               " returns ERROR, stopping execution");
                active = false;
            }

            if (logCurrentNode)
            {
                Debug.Log(blackboard.Get<string>("common_current_node"));
            }
        }
    }
}

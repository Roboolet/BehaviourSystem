using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public bool active = true;
    
    [SerializeField] private BehaviourBlueprint behaviour;
    [Header("Settings")] 
    [SerializeField, Range(0.5f,30)] private float ticksPerSecond = 20;
    
    public Blackboard blackboard;
    
    private INode root;
    // private IEvaluator[] evaluators;
    private float lastTickTime;
    private int tickCounter;

    private void Awake()
    {
        root = behaviour.BuildTree();
    }

    private void Update()
    {
        if (active && lastTickTime + (1 / ticksPerSecond) < Time.time)
        {
            // set some common & useful variables
            float tickDelta = Time.time - lastTickTime;
            lastTickTime = Time.time;
            tickCounter++;
            blackboard.Set("tick_delta", tickDelta);
            blackboard.Set("tick_total", tickCounter);

            if (root.Execute(blackboard) == NodeReturnState.ERROR)
            {
                Debug.LogError("Behaviour tree of " + transform.name + 
                               " returns ERROR, stopping execution");
                active = false;
            }
        }
    }
}

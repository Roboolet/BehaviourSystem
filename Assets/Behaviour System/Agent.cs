using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private bool _Active;
    public bool Active
    {
        get
        {
            return _Active;
        }
        set
        {
            _Active = value;
        }
    }
    
    // [SerializeField] private BehaviourBlueprint behaviour;
    [Header("Settings")] 
    [SerializeField, Range(0.5f,30)] private float ticksPerSecond = 20;
    
    public Blackboard blackboard;
    
    private INode root;
    // private IEvaluator[] evaluators;
    private float lastTickTime;
    private int tickCounter;

    private void Update()
    {
        if (lastTickTime + (1 / ticksPerSecond) < Time.time)
        {
            // set some common & useful variables
            float tickDelta = Time.time - lastTickTime;
            blackboard.Set("tick_delta", tickDelta);
            blackboard.Set("tick_total", tickCounter);
            
            root.Execute(blackboard);
            lastTickTime = Time.time;
            tickCounter++;
        }
    }
}

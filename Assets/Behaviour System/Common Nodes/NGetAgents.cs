using System;
using System.Collections.Generic;
using UnityEngine;

public class NGetAgents : ANode
{
    private readonly string groupTag;
    private readonly string bbOutput;
    private readonly bool excludeSelf;

    public NGetAgents(string _outputBlackboardKey, string _groupTag = "", bool _excludeSelf = true)
    {
        groupTag = _groupTag;
        bbOutput = _outputBlackboardKey;
        excludeSelf = _excludeSelf;
    }

    protected override NodeReturnState OnExecute(Blackboard bb)
    {
        Agent self = bb.Get<Agent>(CommonBB.AGENT);
        List<Agent> agents = Blackboard.Global.ListGet<Agent>(CommonBB.AGENTS_LIST);
        if (agents == null) return NodeReturnState.ERROR;

        List<GameObject> filteredList = new List<GameObject>();
        
        for (int i = 0; i < agents.Count; i++)
        {
            Agent a = agents[i];
            if(a == null) continue;
            
            bool isGroup = String.IsNullOrEmpty(groupTag) || a.groupTag == groupTag;
            bool notSelf = !excludeSelf || a != self;
            if (isGroup && notSelf)
            {
                filteredList.Add(a.gameObject);
            }
        }
        bb.Set(bbOutput, filteredList);

        return NodeReturnState.SUCCESS;
    }
}

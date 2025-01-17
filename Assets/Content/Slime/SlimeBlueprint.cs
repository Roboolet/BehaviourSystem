using System;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    private const string TARGET = "slime_target";

    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float sizeMinimum, sizeThreshold;
    
    public override INode BuildTree()
    {
        INode branch_chasing = new NDComparison<float>(new NCSequence(
            new INode[]
            {
                new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)
            }), TARGET, Comparator.GREATER, sizeThreshold);
        
        INode root = new NCSequence(new INode[]
        {
            new NGetGameObjectWithTag("Player", TARGET),
            new NDHasLineOfSight(
                new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT),
                TARGET, targetLineOfSightLayer)
        });
        
        return root;
    }
}

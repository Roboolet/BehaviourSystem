using System;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    private const string TARGET = "slime_target";
    private const string TARGET_DISTANCE = "slime_target_distance";
    private const string SIZE = "slime_size";
    private const string IS_PATROLLING = "slime_is_patrolling";

    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float sizeMinimum, sizeThreshold, attackDistance;
    
    public override INode BuildTree()
    {
        // the red section on the diagram
        INode branch_chasing = new NDComparison<float>(new NCSequence(
            new INode[]
            {
                new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT),
                new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
                // if close to player, attack
                new NDComparison<float>(new NAttackPlayer(), TARGET_DISTANCE, Comparator.GREATER,
                    attackDistance, true)
            }), SIZE, Comparator.GREATER, sizeThreshold);

        // the blue section on the diagram
        INode branch_patrolling = new NCSequence(
            new INode[]
            {
                new NCSelector(
                    new INode[]
                    {
                        // if not patrolling, go to closest patrol point
                        new NDReadBool(
                            new NCSequence(
                                new INode[]
                                {   
                                    new NBlackboardSet(IS_PATROLLING, true)
                                })
                            , IS_PATROLLING, true)
                    })
            });
        
        INode root = new NCSequence(new INode[]
        {
            new NGetGameObjectWithTag("Player", TARGET),
            new NCSelector(
                new INode[]
            {
                // the slime sees the player, now decide to run or attack
                new NDHasLineOfSight(
                    new NCSequence(
                        new INode[]
                    {
                        new NCSelector(
                            new INode[]
                            {
                                branch_chasing
                            }) 
                        
                    }), TARGET, targetLineOfSightLayer)
            })
        });
        
        return root;
    }
}

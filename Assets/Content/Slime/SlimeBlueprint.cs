using System;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    private const string TARGET = "slime_target";
    private const string TARGET_DISTANCE = "slime_target_distance";
    private const string ALL_SLIMES = "slime_all_slimes";
    private const string SIZE = "slime_size";
    private const string IS_PATROLLING = "slime_is_patrolling";
    private const string HAS_LOS = "slime_has_los";

    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float sizeMinimum, sizeThreshold, attackDistance, fuseDistance;
    
    public override INode BuildTree()
    {
        // the blue section on the diagram
        INode branch_patrolling = new NCSequence(
            new NCSelector(
                // if not patrolling, go to closest patrol point
                new NDReadBool(
                    new NCSequence(
                        new NGetClosestPatrolPoint(TARGET),
                        new NBlackboardSet(IS_PATROLLING, true))
                    , IS_PATROLLING, true), 
                new NCSequence(
                    // when close to target patrol point, go to the next patrol point
                    new NGetClosestPatrolPoint(TARGET),
                    new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
                    new NDComparison<float>(
                        new NGetNextPatrolPoint(TARGET, TARGET),
                        TARGET_DISTANCE, Comparator.GREATER, 2, true))
            ),
            new NPrint("Moving towards patrol point"),
            new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT));
        
        // the red section on the diagram
        INode branch_chasing = new NDComparison<float>(
            new NCSequence(
                new NPrint("Moving towards player"),
                new NCSelector(
                    new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT),
                    new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
                    // if close to player, attack
                    new NDComparison<float>(new NAttackPlayer(), TARGET_DISTANCE, Comparator.GREATER,
                    attackDistance, true)
            )), SIZE, Comparator.GREATER, sizeThreshold);

        // the yellow section on the diagram
        INode branch_fleeing = new NDComparison<float>(new NCSelector(
            // fusing
            new NCSequence(
                new NGetAgents(ALL_SLIMES, "Enemy"),
                new NGetClosestGameObjectInList(ALL_SLIMES, TARGET),
                new NCSequence(
                    new NPrint("Moving towards other slime"),
                    new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT),
                    new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
                    new NDComparison<float>(
                        new NSlimeFuse(TARGET, SIZE), 
                        TARGET_DISTANCE, Comparator.GREATER, fuseDistance, true)))
        ), SIZE, Comparator.GREATER, sizeThreshold, true);

        INode root = new NCSequence(
            new NGetGameObjectWithTag("Player", TARGET),
            new NGetLineOfSight(TARGET, HAS_LOS, targetLineOfSightLayer),
            new NCSelector(
                // the slime sees the player, now decide to run or attack
                new NDReadBool(
                    new NCSequence(
                    new NBlackboardSet(IS_PATROLLING, false),
                    new NCSelector(
                        branch_chasing,
                        branch_fleeing)), HAS_LOS)
                ,
                new NDReadBool(branch_patrolling, HAS_LOS, true)));
        
        return root;
    }
}

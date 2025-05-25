using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Ninja", menuName = "BehaviourBlueprint/Ninja", order = -1)]
public class NinjaBlueprint : BehaviourBlueprint
{
    private const string ENEMY_AGENTS = "ninja_enemies";
    private const string ENEMY_CLOSEST = "ninja_enemy_closest";
    private const string ENEMY_CLOSEST_DISTANCE = "ninja_enemy_closest_distance";
    private const string TARGET = "ninja_target";
    private const string TARGET_DISTANCE = "ninja_target_distance";
    private const string SMOKE_TIMER = "ninja_smoke_bomb_cooldown";
    private const string HAS_LOS = "ninja_has_los";

    [SerializeField] private string playerTag;
    [SerializeField] private string enemiesGroupTag;
    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float playerMinDistance, playerMaxDistance,
        enemyMinDistance, bombTriggerDistance, smokeCooldown, smokeThrowWaitTime;
    [SerializeField] private GameObject smokeBombPrefab;
    [SerializeField] private NavMeshQueryFilter navFilter;

    public override INode BuildTree()
    {
        INode throwBomb = new NCSequence(
            new NTimerSet(SMOKE_TIMER),
            new NThrowSmokeBomb(smokeBombPrefab, TARGET),
            new NStopMoving(true),
            new NWait(smokeThrowWaitTime));
        
        INode root = new NCSequence(
            new NGetGameObjectWithTag(playerTag, TARGET), 
            new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
            new NGetLineOfSight(TARGET, HAS_LOS, targetLineOfSightLayer),
            new NCSelector(
                    // main selector
                    new NDReadBool(
                    new NCSelector(
                        // smoke bomb is off cooldown
                        new NDTimerCheck(
                            // throw bomb when an enemy is close to the player
                            new NCSequence(
                                new NGetAgents(ENEMY_AGENTS, enemiesGroupTag),
                                new NGetClosestGameObjectInList(ENEMY_AGENTS, ENEMY_CLOSEST, TARGET),
                                new NGetDistanceTo(ENEMY_CLOSEST, ENEMY_CLOSEST_DISTANCE, 
                                    PositionReadMode.GAME_OBJECT, TARGET),
                                new NDComparison<float>(throwBomb, ENEMY_CLOSEST_DISTANCE,
                                    Comparator.GREATER, bombTriggerDistance, true))
                            , SMOKE_TIMER, smokeCooldown),
                        
                        // player is too far
                        new NDComparison<float>(
                            new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)
                            , TARGET_DISTANCE, Comparator.GREATER, playerMaxDistance),
                        
                        // player is too close
                        new NDComparison<float>(
                            new NCSequence(
                                new NMoveAwayFrom(TARGET, navFilter, playerMinDistance + 0.5f),
                                new NWait(1)),
                            TARGET_DISTANCE, Comparator.GREATER, playerMinDistance, true),
                        
                        // TODO: move away from nearby enemies / throw bomb at self
                        //new NCSequence()
                        
                        // stop ninja from walking too close
                        new NStopMoving()
                        ), 
                    HAS_LOS),
                    new NDReadBool(
                        new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT), HAS_LOS, true)
                
                ));
        return root;
    }
}

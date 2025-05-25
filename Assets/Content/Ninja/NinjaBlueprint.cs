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

    [SerializeField] private string playerTag;
    [SerializeField] private string enemiesGroupTag;
    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float playerMinDistance, playerMaxDistance,
        enemyMinDistance, bombTriggerDistance, smokeCooldown;
    [SerializeField] private GameObject smokeBombPrefab;
    [SerializeField] private NavMeshQueryFilter navFilter;

    public override INode BuildTree()
    {
        INode throwBomb = new NCSequence(
            new NTimerSet(SMOKE_TIMER),
            new NThrowSmokeBomb(smokeBombPrefab, TARGET_DISTANCE));
        
        INode root = new NCSequence(
            new NGetAgents(ENEMY_AGENTS, enemiesGroupTag),
            new NGetGameObjectWithTag(playerTag, TARGET), 
            new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT),
            new NCSelector(
                new NDHasLineOfSight(
                    // main selector
                    new NCSelector(
                        // move away from too close player
                        new NDComparison<float>(
                            new NMoveAwayFrom(TARGET, navFilter),
                            TARGET_DISTANCE, Comparator.GREATER, playerMinDistance, true),
                        // smoke bomb is off cooldown
                        new NDTimerCheck(
                            new NCSelector(
                                new NDComparison<float>(
                                    new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)
                                    , TARGET_DISTANCE, Comparator.GREATER, playerMaxDistance),
                                // throw bomb when an enemy is close to the player
                                new NCSequence(
                                    new NGetClosestGameObjectInList(ENEMY_AGENTS, ENEMY_CLOSEST, TARGET),
                                    new NGetDistanceTo(ENEMY_CLOSEST, ENEMY_CLOSEST_DISTANCE, 
                                        PositionReadMode.GAME_OBJECT, TARGET),
                                    new NDComparison<float>(throwBomb, ENEMY_CLOSEST_DISTANCE,
                                        Comparator.GREATER, bombTriggerDistance, true)))
                            , SMOKE_TIMER, smokeCooldown)
                        // TODO: move away from nearby enemies / throw bomb at self
                        //new NCSequence()
                        ), TARGET, targetLineOfSightLayer),
                new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)));
        return root;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Ninja", menuName = "BehaviourBlueprint/Ninja", order = -1)]
public class NinjaBlueprint : BehaviourBlueprint
{
    private const string ENEMY_POSITIONS = "ninja_enemies";
    private const string TARGET = "ninja_target";
    private const string TARGET_DISTANCE = "ninja_target_distance";
    private const string SMOKE_TIMER = "ninja_smoke_bomb_cooldown";

    [SerializeField] private string playerTag, enemiesTag;
    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float playerMinDistance, playerMaxDistance,
        enemyMinDistance, smokeCooldown;
    [SerializeField] private GameObject smokeBombPrefab;

    public override INode BuildTree()
    {
        INode root = new NCSequence(
            // TODO: get agent in group
            new NTimerSet(SMOKE_TIMER),
            new NGetGameObjectWithTag(playerTag, TARGET), 
            new NCSelector(
                new NDHasLineOfSight(
                    new NCSelector(
                        new NDComparison<float>(
                            // TODO: move away from player
                            new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)
                            , TARGET_DISTANCE, Comparator.GREATER, playerMinDistance, true),
                        new NDTimerCheck(new NCSelector(
                                new NCSequence(
                                    new NGetDistanceTo(TARGET, TARGET_DISTANCE, PositionReadMode.GAME_OBJECT), 
                                    new NDComparison<float>(
                                        new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)
                                        , TARGET_DISTANCE, Comparator.GREATER, playerMaxDistance)))
                            , SMOKE_TIMER, smokeCooldown),
                        new NCSequence() // TODO: Get closest gameobject
                        ), TARGET, targetLineOfSightLayer),
                new NMoveTowards(TARGET, PositionReadMode.GAME_OBJECT)));

        // TODO: throw smoke bomb / instantiate node
        //INode throwBomb = new NCSequence()
        return root;
    }
}

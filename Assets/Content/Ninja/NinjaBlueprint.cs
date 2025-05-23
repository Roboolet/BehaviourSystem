using UnityEngine;

[CreateAssetMenu(fileName = "Ninja", menuName = "BehaviourBlueprint/Ninja", order = -1)]
public class NinjaBlueprint : BehaviourBlueprint
{
    private const string ENEMY_POSITIONS = "ninja_enemies";
    private const string TARGET = "ninja_target";
    private const string TARGET_DISTANCE = "ninja_target_distance";
    private const string SMOKE_COOLDOWN = "ninja_smoke_bomb_cooldown";

    [SerializeField] private string playerTag, enemiesTag;
    [SerializeField] private LayerMask targetLineOfSightLayer;
    [SerializeField] private float playerMinDistance, playerMaxDistance,
        enemyMinDistance, smokeCooldown;

    public override INode BuildTree()
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    public float size;
    public float speed;

    private const string TARGET = "slime_target";
    
    public override INode BuildTree()
    {
        INode root = new NCSequence(new INode[]
        {
            new NGetGameObjectWithTag("Player", TARGET),
            new NDHasLineOfSight(
                new NMoveTowards(TARGET, NMoveTowards.PositionReadMode.GAME_OBJECT),
                TARGET)
        });
        
        return root;
    }
}

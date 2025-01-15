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
            new NGetPositionWithTag("Player", TARGET),
            new NPrintBlackboard(PrintMode.LOG, TARGET),
            new NMoveTowards(TARGET)
        });
        
        return root;
    }
}

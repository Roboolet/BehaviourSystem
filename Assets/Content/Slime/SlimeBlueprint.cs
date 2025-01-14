using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    public float size;
    public float speed;
    
    public override INode BuildTree()
    {
        INode root = new NCSequence(new INode[]
        {
            new NPrint(PrintMode.LOG, "Test Test 123"),
            new NWait(1)
        });

        return root;
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    public float size;
    public float speed;
    
    public override INode BuildTree()
    {
        INode root = new SequenceNode(new INode[]
        {
            new PrintNode(PrintNode.PrintMode.LOG, "Test Test 123"),
            new WaitNode(1)
        });

        return root;
    }
}

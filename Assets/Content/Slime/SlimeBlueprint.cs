using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourBlueprint/Slime", order = -1)]
public class SlimeBlueprint : BehaviourBlueprint
{
    public float size;
    public float speed;
    
    public override INode BuildTree()
    {
        throw new System.NotImplementedException();
    }
}

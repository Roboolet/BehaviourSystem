using UnityEngine;

public class NThrowSmokeBomb : NInstantiate
{
    private readonly string bbTarget;
    private readonly PositionReadMode posMode;
    
    public NThrowSmokeBomb(GameObject _prefab,
        string _targetGameObjectBlackboardKey)
        : base(_prefab)
    {
        bbTarget = _targetGameObjectBlackboardKey;
    }

    protected override void ModifyInstantiatedObject(Blackboard _bb, GameObject _gameObject)
    {
        SmokeBomb bomb = _gameObject.GetComponent<SmokeBomb>();
        GameObject go = _bb.Get<GameObject>(bbTarget);
        bomb.Launch(go);
    }
}

using UnityEngine;

public class NThrowSmokeBomb : NInstantiate
{
    private readonly string bbTargetDist;
    
    public NThrowSmokeBomb(GameObject _prefab, string _targetDistanceBlackboardKey)
        : base(_prefab, _rotMode: InstantiateRotationMode.AGENT_INHERIT)
    {
        bbTargetDist = _targetDistanceBlackboardKey;
    }

    protected override void ModifyInstantiatedObject(Blackboard _bb, GameObject _gameObject)
    {
        SmokeBomb bomb = _gameObject.GetComponent<SmokeBomb>();
        bomb.LaunchForward(_bb.Get<float>(bbTargetDist));
    }
}

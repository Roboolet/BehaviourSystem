using UnityEngine;

public abstract class BehaviourBlueprint : ScriptableObject
{
    public BlackboardInitialValue[] blackboardValues;

    /// <summary>
    /// When implemented: returns the node tree of the entity for use by the Agent
    /// </summary>
    /// <returns></returns>
    public abstract INode BuildTree();
    
    //public abstract IEvaluator[] BuildEvaluators();
}

[System.Serializable]
public struct BlackboardInitialValue
{
    public string key;
    public BlackboardValueType type;
    
    // each variable is seperated so that it can be configured in the inspector
    // TODO: make a custom editor for this (because it sucks)
    [Header("Values")]
    public string _string;
    public int _int;
    public float _float;
    public Vector3 _vector3;
}

public enum BlackboardValueType
{
    STRING, INT, FLOAT, VECTOR3
}

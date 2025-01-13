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

public struct BlackboardInitialValue
{
    public string key;
    public BlackboardValueType type;
    
    // each variable is seperated so that it can be configured in the inspector
    public string v_string;
    public int v_int;
    public float v_float;
    public Vector3 v_vector3;
}

public enum BlackboardValueType
{
    STRING, INT, FLOAT, VECTOR3
}

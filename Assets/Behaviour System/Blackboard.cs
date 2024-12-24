using System.Collections;
using UnityEngine;

public class Blackboard
{
    public static Blackboard global;
    private Hashtable objects;

    public Blackboard()
    {
        objects = new Hashtable();
    }
    
    public void Set(string key, object value)
    {
        if (objects.ContainsKey(key))
        {
            objects[key] = value;
        }
        else
        {
            objects.Add(key, value);
        }
    }

    public bool TryGet<T>(string key, out T value)
    {
        if (!objects.ContainsKey(key))
        {
            value = default(T);
            return false;
        }
        else
        {
            value = (T)objects[key];
            return true;
        }
    }

    public bool ContainsKey(string key) => objects.ContainsKey(key);
}

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
            Debug.LogWarning("Blackboard does not contain item with key \""+key);
            return false;
        }
        else
        {
            object obj = objects[key];
            if (obj.GetType() == typeof(T))
            {
                value = (T)objects[key];
                return true;
            }
            else
            {
                Debug.LogWarning("Blackboard does not contain item with key \""+key + "\" that matches Type "+ typeof(T).Name);
                value = default(T);
                return false;
            }
        }
    }

    public T Get<T>(string key)
    {
        if (!objects.ContainsKey(key))
        {
            Debug.LogWarning("Blackboard does not contain item with key \""+key + "\"");
            return default(T);
        }
        else
        {
            object obj = objects[key];
            if (obj.GetType() == typeof(T))
            {
                return (T)objects[key];
            }
            else
            {
                Debug.LogWarning("Blackboard does not contain item with key \""+key + "\" that matches Type "+ typeof(T).Name);
                return default(T);
            }
        }
    }

    public bool ContainsKey(string key) => objects.ContainsKey(key);
}

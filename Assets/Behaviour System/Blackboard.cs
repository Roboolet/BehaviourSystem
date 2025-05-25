using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    public static Blackboard global;
    private Hashtable objects;

    public Blackboard()
    {
        objects = new Hashtable();
    }
    
    public void Set(string _key, object _value)
    {
        if (objects.ContainsKey(_key))
        {
            objects[_key] = _value;
        }
        else
        {
            objects.Add(_key, _value);
        }
    }

    public bool TryGet<T>(string _key, out T _value)
    {
        if (!objects.ContainsKey(_key))
        {
            _value = default(T);
            Debug.LogWarning("Blackboard does not contain item with key \""+_key);
            return false;
        }
        else
        {
            object obj = objects[_key];
            if (obj.GetType() == typeof(T))
            {
                _value = (T)objects[_key];
                return true;
            }
            else
            {
                Debug.LogWarning("Blackboard does not contain item with key \""+_key + "\" that matches Type "+ typeof(T).Name);
                _value = default(T);
                return false;
            }
        }
    }

    public T Get<T>(string _key)
    {
        if (!objects.ContainsKey(_key))
        {
            Debug.LogWarning("Blackboard does not contain item with key \""+_key + "\"");
            return default(T);
        }
        else
        {
            object obj = objects[_key];
            if (obj.GetType() == typeof(T))
            {
                return (T)objects[_key];
            }
            else
            {
                Debug.LogWarning("Blackboard does not contain item with key \""+_key + "\" that matches Type "+ typeof(T).Name);
                return default(T);
            }
        }
    }

    public void ListAdd<T>(string _listKey, T _value)
    {
        if(!ContainsKey(_listKey))
        {
            List<T> newList = new List<T>();
            newList.Add(_value);
            Set(_listKey, newList);
        }
        else
        {
            List<T> list = Get<List<T>>(_listKey);
            list.Add(_value);
        }
    }

    public List<T> ListGet<T>(string _listKey)
    {
        return Get<List<T>>(_listKey);
    }

    public bool ContainsKey(string _key) => objects.ContainsKey(_key);
}

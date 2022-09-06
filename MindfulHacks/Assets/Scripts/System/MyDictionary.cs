using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyDictionary<TKey, TValue>
{
    public List<MyKeyPair<TKey, TValue>> dictionary;

    public MyDictionary() { dictionary = new List<MyKeyPair<TKey, TValue>>(); }
    public TValue this[TKey _key]
    {
        get
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                if (dictionary[i].a.Equals(_key))
                {
                    return dictionary[i].b;
                }
            }

            return default(TValue);
        }

        set
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                if (dictionary[i].a.Equals(_key))
                {
                    dictionary[i].b = value;
                }
            }
        }
    }
    public int Count { get { return dictionary.Count; } }

    public void Add(TKey _key, TValue _value)
    {
        if (ContainsKey(_key))
        {
            Debug.LogError("Key already exist");
            return;
        }

        dictionary.Add(new MyKeyPair<TKey, TValue>(_key, _value));
    }
    public void Remove(TKey _key)
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            if (dictionary[i].Equals(_key))
            {
                dictionary.RemoveAt(i);
                return;
            }
        }
    }
    public void RemoveAt(int _index)
    {
        dictionary.RemoveAt(_index);
    }
    public void Clear()
    {
        dictionary.Clear();
    }
    public bool ContainsKey(TKey _key)
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            if (dictionary[i].a.Equals(_key))
            {
                return true;
            }
        }
        return false;
    }
    public MyKeyPair<TKey,TValue> ElementAt(int _index)
    {
        return dictionary[_index];
    }
}

[System.Serializable]
public class MyKeyPair<Ta, Tb>
{
    public Ta a;
    public Tb b;
    public MyKeyPair(Ta _a, Tb _b)
    {
        a = _a;
        b = _b;
    }
}
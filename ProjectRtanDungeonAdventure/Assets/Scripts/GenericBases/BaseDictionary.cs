using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseDictionary<T> : MonoBehaviour
{
    protected Dictionary<string, T> dict = new Dictionary<string, T>();

    public void AddDict(string key, T content)
    {
        dict.Add(key, content);
    }

    public virtual T GetDict(string key)
    {
        return dict[key];
    }
}

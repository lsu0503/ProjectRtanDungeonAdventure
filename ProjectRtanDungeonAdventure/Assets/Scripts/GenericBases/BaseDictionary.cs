using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseDictionary : GenericSingleton<BaseDictionary>
{
    protected Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

    public void AddDict(string key, GameObject content)
    {
        dict.Add(key, content);
    }

    public virtual GameObject GetDict(string key)
    {
        GameObject resultObj = Instantiate(dict[key]);
        return resultObj;
    }
}

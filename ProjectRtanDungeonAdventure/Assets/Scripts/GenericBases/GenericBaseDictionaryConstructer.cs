using System;
using UnityEngine;

public class GenericBaseDictionaryConstructer<T> : MonoBehaviour where T : BaseDictionary
{
    [Serializable]
    protected struct DictCell
    {
        public string key;
        public GameObject content;

        public DictCell(string _key, GameObject _content)
        {
            key = _key;
            content = _content;
        }
    }

    [SerializeField] protected DictCell[] contents;

    protected virtual void Awake()
    {
        T targetDict = GetComponent<T>();

        if(targetDict == null)
            targetDict = gameObject.AddComponent<T>();

        foreach(DictCell content in contents)
            targetDict.AddDict(content.key, content.content);

        Destroy(this);
    }
}
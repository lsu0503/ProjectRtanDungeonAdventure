using System;
using UnityEngine;

public class GenericBaseDictionaryConstructer<Dict_T, Cont_T> : MonoBehaviour where Dict_T : BaseDictionary<Cont_T>
{
    [Serializable]
    protected struct DictCell
    {
        public string key;
        public Cont_T content;

        public DictCell(string _key, Cont_T _content)
        {
            key = _key;
            content = _content;
        }
    }

    [SerializeField] protected DictCell[] contents;

    protected virtual void Awake()
    {
        Dict_T targetDict = GetComponent<Dict_T>();

        if(targetDict == null)
            targetDict = gameObject.AddComponent<Dict_T>();

        foreach(DictCell content in contents)
            targetDict.AddDict(content.key, content.content);

        Destroy(this);
    }
}
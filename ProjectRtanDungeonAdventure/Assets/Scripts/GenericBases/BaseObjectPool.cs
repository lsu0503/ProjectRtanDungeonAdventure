using System;
using UnityEngine;
using UnityEngine.Pool;

public class BaseObjectPool : MonoBehaviour
{
    protected ObjectPool<GameObject> pool;
    [SerializeField] protected int minSize;
    [SerializeField] protected int maxSize;


    protected virtual void Awake()
    {
        pool = new ObjectPool<GameObject>(CreateObject, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, true, minSize, maxSize);
    }

    public virtual void OnDestroyPoolObject(GameObject _object)
    {
        Destroy(_object);
    }

    public virtual void OnReturnToPool(GameObject _object)
    {
        _object.SetActive(false);
    }

    public virtual void OnTakeFromPool(GameObject _object)
    {
        _object.SetActive(true);
    }

    public virtual GameObject CreateObject()
    {
        return new GameObject();
    }
}

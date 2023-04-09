using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseFactory<T> : MonoBehaviour where T: Component
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int PoolSize;
    
    protected ObjectPool<T> FactoryObjects;

    protected virtual void Start()
    {
        InitPool(PoolSize, transform);
    }

    protected void InitPool(int count, Transform parent)
    {
        FactoryObjects = new ObjectPool<T>(createFunc: () =>
                Instantiate(Prefab, parent),
            actionOnGet: (obj) => obj.gameObject.SetActive(true), 
            actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: true,
            maxSize: count);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public static ObjectPool Inst { get; } = new ObjectPool();

    private PoolManager pool;

    public ObjectPool()
    {
        pool = new PoolManager("ModelPool");
    }

    public GameObject Create(string resPath)
    {
        return pool.De(resPath);
    }

    public void Destroy(GameObject obj)
    {
        pool.En(obj);
    }
}
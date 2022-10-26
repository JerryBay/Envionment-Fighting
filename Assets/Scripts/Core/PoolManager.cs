using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public readonly Transform Parent;

    private Dictionary<string, PoolHandler> pools;
    private PoolHandler tempHandler;

    public PoolManager(string parentName)
    {
        pools = new Dictionary<string, PoolHandler>();
        Parent = new GameObject(parentName).transform;
        GameObject.DontDestroyOnLoad(Parent.gameObject);
    }

    public GameObject De(string resPath, bool active = true)
    {
        if (!pools.TryGetValue(resPath, out tempHandler))
            pools[resPath] = tempHandler = new PoolHandler(resPath, Parent);
        GameObject obj = tempHandler.De();
        obj.SetActive(active);
        return obj;
    }

    public void En(GameObject obj)
    {
        if (pools.TryGetValue(obj.name, out tempHandler))
        {
            obj.SetActive(false);
            tempHandler.En(obj);
        }
    }
}

public class PoolHandler
{
    private Transform parent;
    private string resPath;
    private Queue<GameObject> objs;

    public PoolHandler(string resPath, Transform parent)
    {
        this.resPath = resPath;
        this.parent = parent;
        objs = new Queue<GameObject>();
    }

    public GameObject De()
    {
        if (objs.Count > 0)
            return objs.Dequeue();
        return CreateNew();
    }

    public void En(GameObject obj)
    {
        if (!objs.Contains(obj))
        {
            obj.transform.SetParent(parent);
            objs.Enqueue(obj);
        }
    }

    private GameObject CreateNew()
    {
        GameObject obj = Resources.Load<GameObject>(resPath);
        obj = Object.Instantiate(obj, parent);
        obj.name = resPath;
        return obj;
    }
}
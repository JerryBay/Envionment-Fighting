using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : SingletonMono<BuffManager>
{
    public List<BuffBase> buffs;

    private void Awake()
    {
        buffs = new List<BuffBase>();
    }

    private void Update()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].BuffUpdate();
        }
    }

    public void AddBuff(BuffBase buff)
    {
        if (!buffs.Contains(buff))
        {
            buffs.Add(buff);
        }
    }

    public void RemoveBuff(BuffBase buff)
    {
        if (buffs.Contains(buff))
        {
            buffs.Remove(buff);
        }
    }
}
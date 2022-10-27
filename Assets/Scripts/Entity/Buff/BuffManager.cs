using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : SingletonMono<BuffManager>
{
    public List<BuffBase> buffs;

    public delegate void BuffCall();

    public BuffCall buffCall;
    public int buffQuantity;

    private void Awake()
    {
        buffs = new List<BuffBase>();
        buffCall = delegate() { };
        buffQuantity = 0;
    }

    public bool AddBuff(BuffBase buff)
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i] == null)
            {
                buffs[i] = buff;
                buffCall += buffs[i].BuffEffect; //添加响应函数
                buffQuantity++;
                buffs[i].BuffStart();
                return true;
            }
        }

        return false;
    }

    public bool RemoveBuff(int index)
    {
        if (buffs[index] == null)
        {
            return false;
        }

        buffs[index].BuffEnd();
        buffCall -= buffs[index].BuffEffect;
        buffQuantity--;

        while (index < buffs.Count)
        {
            buffs[index] = null;
            buffs[index] = index + 1 >= buffs.Count ? null : buffs[index + 1];
            index++;
        }

        return true;
    }
}
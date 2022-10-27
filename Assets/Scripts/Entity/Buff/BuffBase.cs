using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBase
{
    public BuffData data;

    public virtual void BuffStart()
    {
    }

    public virtual void BuffEffect()
    {
    }

    public virtual void BuffEnd()
    {
    }

    public void SetBuffData(BuffData inputData)
    {
        if (data != null || inputData == null)
        {
            return;
        }

        data = inputData;
    }
}
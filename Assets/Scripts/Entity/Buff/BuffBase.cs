using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase
{
    public BuffData data;
    public int buffLevel;

    public abstract bool BuffStart();

    public abstract bool BuffEffect();

    public abstract bool BuffEnd();

    public abstract bool BuffUpdate();

    public void SetBuffData(BuffData inputData)
    {
        data = inputData;
    }
}
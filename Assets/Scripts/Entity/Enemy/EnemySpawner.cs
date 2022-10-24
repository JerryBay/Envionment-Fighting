using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class EnemySpawner
{
    public abstract BaseEnemy Spawn(float2 pos,float2 dir);
}

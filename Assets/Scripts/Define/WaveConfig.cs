using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New WaveConfig", menuName = "创建波次敌人")]
public class WaveConfig : ScriptableObject
{
    public BaseEnemy enemyType;
    public Route route;
    public int count;
    public float interval;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buff Data", fileName = "BuffData")]
public class BuffData : ScriptableObject
{
    public int maxLevel;
    
    public string buffName;

    public float[] buffValue = new float[10];
    
    public float[] duration = new float[10];

    [TextArea(2, 3)]
    public string description;
}

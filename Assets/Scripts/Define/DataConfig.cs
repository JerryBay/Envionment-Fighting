using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data Configure" ,menuName = "创建数据配置")]

public class DataConfig : ScriptableObject
{
    //初始资产
    [Space]
    public float primaryCoin;
    public int primaryPopulation;
    public List<GameObject> primaryEntity;

    //污染阈值
    [Space]
    public float lightPollute;
    public float midPollute;
    public float heavyPollute;
    
    //人口增长速率
    [Space]
    public int fixedIncrement;
    public int ratioIncrement;
}

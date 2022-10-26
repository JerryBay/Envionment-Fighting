using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    public DataConfig config;
    
    public float productivity;

    public float population;
    
    public float polluteRate;

    public float polluteTotal;

    public float happiness;
}

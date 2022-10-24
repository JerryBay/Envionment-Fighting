using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public Category layer;
    
    public float totalHealth = 100;
    public float speed = 10;

    private float _curHealth;

    protected virtual void Start()
    {
        _curHealth = totalHealth;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(float damage)
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public DefenseTower originTower;
    public BaseEnemy targetEnemy;
    public AmmoType type;
    public float speed;
    public float damage;
    
    public GameObject explosionEffectPrefab;
    
    private float _minDist = 0.02f;

    private void Update()
    {
        if (targetEnemy == null)
        {
            DestroyAmmo();
            return;
        }
        
        Vector3 dir = (targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(dir * Time.deltaTime * speed);
        if (Vector2.Distance(targetEnemy.transform.position,transform.position)<_minDist)
        {
            if (type == AmmoType.AreaOfEffect)
            {
                
            }
            targetEnemy.TakeDamage(damage);
            DestroyAmmo();   
        }
    }

    private void DestroyAmmo()
    {
        Destroy(gameObject);
    }

    // public void SetOriginTower(DefenseTower tower)
    // {
    //     originTower = tower;
    // }
    //
    // public void SetTargetEnemy(BaseEnemy enemy)
    // {
    //     targetEnemy = enemy;
    // }
}

public enum AmmoType
{
    SingleAttack,
    AreaOfEffect
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public DefenseTower originTower;
    public BaseEnemy targetEnemy;
    public AmmoType type;
    public Area area;
    public float range;
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
        if (Vector2.Distance(targetEnemy.transform.position, transform.position) < _minDist)
        {
            if (type == AmmoType.AreaOfEffect)
            {
                Vector2 center = targetEnemy.transform.position;
                var enemies = EnemyManager.Instance.enemies;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (Vector2.Distance(enemies[i].transform.position, center) <= range)
                    {
                        if ((enemies[i].area & area) != 0)
                        {
                            enemies[i].TakeDamage(damage);
                        }
                    }
                }
                AudioManager.Instance.PlayEffect("AOE",false);
            }
            else
            {
                targetEnemy.TakeDamage(damage);
                AudioManager.Instance.PlayEffect("Single",false);
            }

            DestroyAmmo();
        }
    }

    private void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}

public enum AmmoType
{
    SingleAttack,
    AreaOfEffect
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    public Area area;

    public float totalHealth = 100;
    public float speed = 10;

    //private Slider _healthSlider;
    private List<Vector2> _wayPoints;
    [SerializeField]
    private float _curHealth;
    private int _pointIndex;

    private void Start()
    {
        _curHealth = totalHealth;
        //_healthSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_pointIndex > _wayPoints.Count - 1)
        {
            return;
        }
        
        transform.Translate((_wayPoints[_pointIndex] - (Vector2)transform.position)
                            .normalized * Time.deltaTime * speed);
        if (Vector2.Distance(_wayPoints[_pointIndex],transform.position) < 0.02f)
        {
            _pointIndex++;
        }
    }
    

    public void SetWayPoints(List<Vector2> points)
    {
        _wayPoints = points;
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        //_healthSlider.value = _curHealth / totalHealth;
        if (_curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

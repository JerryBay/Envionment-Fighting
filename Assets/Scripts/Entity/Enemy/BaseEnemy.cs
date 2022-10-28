using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
    public Area area;

    public float totalHealth;
    public float speed;
    public float damage;
    public SlowDown slowDown;
    public bool startToAttack = false;

    //private Slider _healthSlider;
    private List<Vector2> _wayPoints;
    [SerializeField] private float _curHealth;
    private int _pointIndex;

    private void Awake()
    {
        EnemyManager.Instance.enemies.Add(this);
    }

    private void Start()
    {
        _curHealth = totalHealth;
        //_healthSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        Move();
        if (_pointIndex >= _wayPoints.Count)
        {
            startToAttack = true;
        }
    }

    private void OnDestroy()
    {
        if (EnemyManager.Instance.enemies.Contains(this))
        {
            EnemyManager.Instance.enemies.Remove(this);
        }
    }

    private void Move()
    {
        if (_pointIndex >= _wayPoints.Count)
        {
            return;
        }

        Vector2 moveDir = _wayPoints[_pointIndex] - (Vector2) transform.position;
        if(moveDir.x != 0)
            transform.localScale=new Vector3(-Mathf.Sign(moveDir.x), 1, 1);
        transform.Translate(moveDir.normalized * Time.deltaTime * speed);
        if (Vector2.Distance(_wayPoints[_pointIndex], transform.position) < 0.02f)
        {
            _pointIndex++;
        }
    }


    public void SetWayPoints(List<Vector2> points)
    {
        _wayPoints = points;
        if (points != null && points.Count > 0)
            transform.position = _wayPoints[0];
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
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
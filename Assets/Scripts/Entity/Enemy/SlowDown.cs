// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class SlowDown : MonoBehaviour
// {
//     public IntervalTimer timer;
//     public BaseEnemy enemy;
//     
//     public float ratio;
//     public float duration;
//
//     private void Awake()
//     {
//         Attach(enemy);
//     }
//
//     private void Update()
//     {
//         timer.Update();
//     }
//
//     private void OnDestroy()
//     {
//         Detach(enemy);
//     }
//
//     public void Attach(BaseEnemy enemy)
//     {
//         if (enemy == null)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         // if (Compare(enemy.slowDown))
//         // {
//             if (enemy.slowDown)
//             {
//                 Destroy(enemy.slowDown);
//             }
//             enemy.slowDown = this;
//             enemy.speed *= 1 - ratio;
//             timer = new IntervalTimer(duration);
//             timer.action = () => { Destroy(gameObject); };
//         // }
//         // else
//         // {
//         //     Destroy(this);
//         // }
//     }
//
//     public bool Compare(SlowDown other)
//     {
//         if (other == null)
//         {
//             return true;
//         }
//         if (this.ratio > other.ratio)
//         {
//             return true;
//         }
//         else if(this.ratio < other.ratio)
//         {
//             return false;
//         }
//         else
//         {
//             if (this.duration > other.timer.GetTime())
//             {
//                 return true;
//             }
//             else
//             {
//                 return false;
//             }      
//         }
//     }
//
//     public void Detach(BaseEnemy enemy)
//     {
//         if (enemy)
//         {
//             enemy.speed /= 1 - ratio;
//         }
//     }
// }

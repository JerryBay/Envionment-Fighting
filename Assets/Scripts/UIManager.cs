// using System;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
//
// public class UIManager : MonoBehaviour
// {
//     public TextMeshProUGUI productivity;
//
//     public TextMeshProUGUI welfare;
//
//     public TextMeshProUGUI pollution;
//
//     private void OnEnable()
//     {
//         // 注册ui事件！
//         EventManager.Register("ProductivityUpdate", OnProductivityUpdateEvent);
//         EventManager.Register("PollutionUpdate", OnPollutionUpdateEvent);
//         EventManager.Register("WelfareUpdate", OnWelfareUpdateEvent);
//     }
//
//     private void OnDisable()
//     {
//         // 注销ui事件！
//         EventManager.Unregister("ProductivityUpdate", OnProductivityUpdateEvent);
//         EventManager.Unregister("PollutionUpdate", OnPollutionUpdateEvent);
//         EventManager.Unregister("WelfareUpdate", OnWelfareUpdateEvent);
//     }
//
//     // 更新生产力！
//     private void OnProductivityUpdateEvent(string[] args)
//     {
//         productivity.text = args[0];
//     }
//
//     // 更新污染情况！
//     private void OnPollutionUpdateEvent(string[] args)
//     {
//         pollution.text = args[0];
//     }
//
//     // 更新生活水准！
//     private void OnWelfareUpdateEvent(string[] args)
//     {
//         welfare.text = args[0];
//     }
// }

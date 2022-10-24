using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiMgr;
    private GlobalData _globalData;

    public void UpdateData(GlobalData data)
    {
        _globalData = data;
    }

    public void UpdateUI()
    {
        
    }
}

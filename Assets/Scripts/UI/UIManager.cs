using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Inst { get; } = new UIManager();

    public Transform UIParent { get { return uiParent; } }
    private Transform uiParent;

    // public Transform UpperParent { get { return upperParent; } }
    // private Transform upperParent;

    private Canvas canvas;

    private Dictionary<string, UIPanelBase> panelDict = new Dictionary<string, UIPanelBase>();

    public UIManager()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        uiParent = GameObject.Find("UI Parent").transform;
        uiParent.SetParent(canvas.transform);
        // upperParent = GameObject.Find("Upper Parent").transform;
        GameObject.DontDestroyOnLoad(uiParent.gameObject);

        EventManager.Register(GameEvent.GameStageUpdate, OnGameStageUpdateEvent);
    }

    ~UIManager()
    {
        EventManager.Unregister(GameEvent.GameStageUpdate, OnGameStageUpdateEvent);
    }

    private void OnGameStageUpdateEvent(object[] args)
    {
        int stage = (int) (GameStage) args[0];
        if (!GameDef.stagePanels.TryGetValue(stage, out string[] names))
        {
            Debug.LogError("不对了, 不应该没配置");
            return;
        }

        Queue<string> delQue = new Queue<string>();
        foreach (string uiName in panelDict.Keys)
        {
            delQue.Enqueue(uiName);
        }

        while (delQue.Count > 0)
        {
            CloseUIPanel(delQue.Dequeue());
        }

        for (int i = 0; i < names.Length; i++)
        {
            OpenUIPanel(names[i]);
        }
    }

    /// <summary>
    /// 初始化ui
    /// </summary>
    public void Init()
    {
    }

    /// <summary>
    /// 打开UI,如果没有创建则创建.
    /// </summary>
    public T OpenUIPanel<T>() where T : UIPanelBase
    {
        string panelName = typeof(T).Name;
        UIPanelBase panel;
        if (!panelDict.TryGetValue(panelName, out panel))
        {
            panel = FactoryPanel<T>();
            panelDict[panelName] = panel;
        }

        panel.Show();
        return panel as T;
    }

    /// <summary>
    /// 打开UI,如果没有创建则创建.
    /// </summary>
    public UIPanelBase OpenUIPanel(string panelName)
    {
        UIPanelBase panel;
        if (!panelDict.TryGetValue(panelName, out panel))
        {
            panel = FactoryPanel(panelName);
            panelDict[panelName] = panel;
        }

        panel.Show();
        return panel as UIPanelBase;
    }

    /// <summary>
    /// 获取UI,如果没有就返回Null.
    /// </summary>
    public T GetUIPanel<T>() where T : UIPanelBase
    {
        string panelName = typeof(T).Name;
        UIPanelBase panel;
        panelDict.TryGetValue(panelName, out panel);
        return panel as T;
    }

    /// <summary>
    /// 隐藏UI.
    /// </summary>
    public void HideUIPanel<T>() where T : UIPanelBase
    {
        string panelName = typeof(T).Name;
        UIPanelBase panel;
        if (panelDict.TryGetValue(panelName, out panel))
        {
            panel.Hide();
        }
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public void CloseUIPanel<T>() where T : UIPanelBase
    {
        string panelName = typeof(T).Name;
        CloseUIPanel(panelName);
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public void CloseUIPanel(string panelName)
    {
        UIPanelBase panel;
        if (panelDict.TryGetValue(panelName, out panel))
        {
            panel.Hide();
            GameObject.Destroy(panel.gameObject);
            panelDict.Remove(panelName);
        }
    }

    /// <summary>
    /// 独立创建UI
    /// 销毁需调用此类中的Destroy_CreateUIPanel函数.
    /// </summary>
    public T CreateUIPanel<T>() where T : UIPanelBase
    {
        string panelName = typeof(T).Name;
        T panel = FactoryPanel<T>();
        panel.Show();
        return panel as T;
    }

    /// <summary>
    /// 独立创建UI
    /// 销毁需调用此类中的Destroy_CreateUIPanel函数.
    /// </summary>
    public UIPanelBase CreateUIPanel(string panelName)
    {
        UIPanelBase panel = FactoryPanel(panelName);
        panel.Show();
        return panel;
    }

    /// <summary>
    /// 销毁独立创建的UI.
    /// </summary>
    public void Destroy_CreateUIPanel(UIPanelBase uiPanel)
    {
        if (uiPanel != null)
        {
            GameObject.Destroy(uiPanel.gameObject);
            uiPanel = null;
        }
    }

    /// <summary>
    /// 创建UI.
    /// </summary>
    private T FactoryPanel<T>() where T : UIPanelBase
    {
        return UIPanelBase.FactoryPanel<T>();
    }

    /// <summary>
    /// 创建UI.
    /// </summary>
    private UIPanelBase FactoryPanel(string panelName)
    {
        return UIPanelBase.FactoryPanel(panelName);
    }
}
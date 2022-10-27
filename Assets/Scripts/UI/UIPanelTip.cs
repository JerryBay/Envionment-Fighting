using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelTip : UIPanelBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private Text titleText;
    [SerializeField] private Text textText;

    private bool closeAvailable;

    protected override void OnShow()
    {
        GameGlobal.IsPause = true;
    }

    protected override void OnHide()
    {
        GameGlobal.IsPause = false;
    }

    private void Update()
    {
        if (closeAvailable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HideTip();
                closeAvailable = false;
            }
        }
    }

    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="text">信息</param>
    public void ShowTip(string text)
    {
        ShowTip("提示信息", text);
    }

    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="text">信息</param>
    public void ShowTip(string title, string text)
    {
        titleText.text = title;
        textText.text = text;
        closeAvailable = false;
        animator.Play("Show");
    }

    private void HideTip()
    {
        animator.Play("Hide");
    }

    public void AnimEvent_CloseAvailable()
    {
        closeAvailable = true;
    }

    public void AnimEvent_Hide()
    {
        Hide();
    }
}

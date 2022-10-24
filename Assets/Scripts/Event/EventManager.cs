using System;
using System.Collections.Generic;

// 事件管理类！
public static class EventManager
{
    private static Dictionary<int, Action<object[]>> listens = new Dictionary<int, Action<object[]>>();

    // 注册事件！
    public static void Register(GameEvent gameEvent, Action<object[]> action)
    {
        if (listens.TryGetValue((int) gameEvent, out Action<object[]> act))
        {
            act += action;
            listens[(int) gameEvent] = act;
        }
        else listens[(int) gameEvent] = action;
    }

    // 注销事件！
    public static void Unregister(GameEvent gameEvent, Action<object[]> action)
    {
        if (listens.TryGetValue((int) gameEvent, out Action<object[]> act))
        {
            act -= action;
            if (act != null)
                listens[(int) gameEvent] = act;
            else listens.Remove((int) gameEvent);
        }
    }

    // 抛出事件！
    public static void Dispath(GameEvent gameEvent, params object[] args)
    {
        if (listens.TryGetValue((int) gameEvent, out Action<object[]> act))
        {
            act.Invoke(args);
        }
    }
}
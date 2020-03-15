using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

/// <summary>
/// 消息（传递）中心
/// 负责UI框架中，所有UI窗体间的数据传值
/// </summary>
public class MessageCenter 
{
    // 定义一个委托. 观察者模式
    public delegate void DelegateMessageDelivery(KeyValueUpdate kv);

    // 声明委托实例. string: 数据大的分类， DelegateMessageDelivery 数据执行委托
    public static Dictionary<string, DelegateMessageDelivery> _dicMessages = new Dictionary<string, DelegateMessageDelivery>();

    /// <summary>
    /// 调用委托，（消息的监听）
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="handler"></param>
    public static void AddMsgListener(string messageType, DelegateMessageDelivery handler)
    {
            if (!_dicMessages.ContainsKey(messageType))
            {
                _dicMessages.Add(messageType, null);
            }
            _dicMessages[messageType] += handler;
    }

    /// <summary>
    /// 取消消息的监听
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="handler"></param>
    public static void RemoveMsgListener(string messageType, DelegateMessageDelivery handler)
    {
        if(_dicMessages.ContainsKey(messageType))
        {
            _dicMessages[messageType] -= handler;
        }
    }

    
    /// <summary>
    /// 取消所有指定消息的监听
    /// </summary>
    public static void RemoverAllMsgListener()
    {
        if(_dicMessages != null)
        {
            _dicMessages.Clear();
        }
    }


    /// <summary>
    /// 委托的实现方法 (发送消息）
    /// </summary>
    /// <param name="messageType"></param>
    /// <param name="kv"></param>
    public static void SendMessage(string messageType, KeyValueUpdate kv)
    {
        DelegateMessageDelivery del;
        if(_dicMessages.TryGetValue(messageType, out del))
        {
            if(del != null)
            {
                // 调用委托
                del(kv);
            }
        }
    }
        
}


/// <summary>
/// 键值更新对
/// 功能：配合委托，实现委托数据传递
/// </summary>
public class KeyValueUpdate
{
    private string _Key;
    private string _Values;
    public string Key { get { return _Key; } }
    public object Values { get { return _Values; } }

    // 构造函数初始化字段
    public KeyValueUpdate(string key, object valueObj)
    {
        _Key = key;
        _Values = valueObj;
    }
}

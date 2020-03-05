using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 框架核心参数
/// 1. 系统常量
/// 2. 全局方法
/// 3. 系统枚举类型
/// 4. 委托定义
/// </summary>

namespace UIFrame
{
    #region 系统枚举类型
    
    /// <summary>
    /// UI窗体（位置）类型
    /// </summary>
    public enum UIFormType
    {
        Normal,  // 普通窗体
        Fixed,  // 固定窗体
        PopUP  // 弹出窗体
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        Normal,  // 普通
        ReverseChange,  // 反向切换
        HideOther  // 隐藏
    }
    
    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormLucencyType
    {
        Lucency,  // 完全透明， 不能穿透
        Translucence, // 半透明，不能穿透
        ImPenetrable,  // 低透明，不能穿透
        Pentrate  // 可以穿透
    }
    

    #endregion
}


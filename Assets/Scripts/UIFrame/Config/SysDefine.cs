using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 框架核心参数
/// 为了规整，把一些全局性的常量统一放在这里，包括以下：
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

    public class SysDefine : MonoBehaviour
    {
        // 路径常量
        public const string SYS_PATH_CANVAS = "Canvas";


        // 标签常量
        public const string SYS_TAG_CANVAS = "_TagCanvas";

        // 全局性方法
        public void Test()
        {


        }

        // 委托的定义
        // to do ...
    }


}


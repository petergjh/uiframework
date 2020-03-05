using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI管理器
/// 是UI框架的核心功能逻辑
/// </summary>

namespace UIFrame
{

    /// <summary>
    /// 定义UI管理器：单例模式，在游戏对象创建时脚本组件能自动挂载上去
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // 定义字段
        private static UIManager _Instance = null;

        // UI窗体预设路径（参数1：窗体预设名称， 2:窗体预设路径 ）
        private Dictionary<string, string> _DicFormsPaths;

        // 缓存所有UI窗体
        private Dictionary<string, BaseUIForm> _DicAllUIForms;

        // 当前显示的UI窗体
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;

        // UI根节点
        private Transform _TraCanvasTransfrom = null;

        // 全屏幕显示的节点
        private Transform _TraNormal = null;

        // 固定显示的节点
        private Transform _TraFixed = null;

        // 弹出节点
        private Transform _TraPopUp = null;

        // UI管理脚本的节点
        private Transform _TraUIScripts = null;


        // 得到实例
        public static UIManager GetInstance()
        {
            if (_Instance==null)
            {
                _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
            }
            return _Instance;
        }

        public void Awake()
        {
            
        }



    }
}

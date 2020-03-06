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
        // 定义一个单例模式的私有字段，创建实例变量
        private static UIManager _Instance = null;
        // 定义UI窗体预设路径（参数1：窗体预设名称， 2:窗体预设路径 ）
        private Dictionary<string, string> _DicFormsPaths;
        // 缓存所有UI窗体
        private Dictionary<string, BaseUIForm> _DicAllUIForms;
        // 当前已经打开的UI窗体
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
        // 定义UI根节点
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

        // 初始化核心数据，加载“UI窗体路径”到集合中
        public void Awake()
        {
            // 字段初始化
            _DicAllUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _DicFormsPaths = new Dictionary<string, string>();

            // 初始化加载根UI窗体Canvas预设
            InitRootCanvasLoading();

            // 得到UI根节点、全屏节点、固定节点、弹出节点
            _TraCanvasTransfrom = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
            //_TraCanvasTransfrom = GameObject.FindGameObjectWithTag("_TagCanvas").transform;
            _TraNormal = _TraCanvasTransfrom.Find("Normal");
            _TraFixed = _TraCanvasTransfrom.Find("Fixed");
            _TraPopUp = _TraCanvasTransfrom.Find("PopUp");
            _TraUIScripts = _TraCanvasTransfrom.Find("_ScriptMgr");

            // 把本脚本做为“根UI窗体”的子节点
            this.gameObject.transform.SetParent(_TraUIScripts,false);

            // “根UI窗体”在场景转换时不允许销毁
            DontDestroyOnLoad(_TraCanvasTransfrom);

            // 初始化UI“窗体预设”路径数据
            //先写简单的，后面再用json做配置文件
            if(_DicFormsPaths!=null)
            {
                _DicFormsPaths.Add("LogonUIForm", @"UIPrefabs\LogonUIForm");
            }
            
        }

        // 初始化加载根UI窗体Canvas预设
        private void InitRootCanvasLoading()
        {
            // Resources.Load("Canvas"); unity自带的Resource类效率太低，把缓存加进去封装一下
             ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
        }



        /// <summary>
        /// 打开UI窗体的公共方法
        /// 功能：
        /// 1. 根据UI窗体的名称找到相应路径进行加载到缓存集合
        /// 2. 根据定义好的显示模式进行加载不同的UI窗体
        /// </summary>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUIForms=null;             // UI窗体基类
            // 参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;

            // 根据UI窗体的名称找到相应路径加载到缓存集合
            baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
            if (baseUIForms == null) return;

            // 根据定义好的显示模式加载不同的UI窗体
            Debug.Log(baseUIForms);

            Debug.Log(baseUIForms.CurrentUIType.UIForms_ShowMode);
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormShowMode.Normal:          // 普通显示窗口模式
                    // 把当前窗体加载到“当前窗体”集合中
                    LoadUIToCurrentCache(uiFormName);
                    // Todo...
                    break;
                case UIFormShowMode.ReverseChange:   // 需要反向切换窗口模式
                    // Todo...
                    break;
                case UIFormShowMode.HideOther:       // 隐藏其它窗口模式
                    // Todo...
                    break;
                default:
                    break;
            }
        }

               
        /// <summary>
        /// 根据UI窗体的名称找到相应路径加载到缓存集合
        /// 功能： 检查所有UI窗体集合中，是否已经加载过了，否则才加载
        /// </summary>
        /// <param name="uiFormsName">UI窗体预设的名称</param>
        /// <returns></returns>
        private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFormsName)
        {
            // 检查之前是否加载过，如果没有就加载
            BaseUIForm baseUIResult = null; // 加载的返回UI窗体基类

            _DicAllUIForms.TryGetValue(uiFormsName, out baseUIResult);
            if (baseUIResult == null)
            {
                // 加载指定路径的 UI窗体
                baseUIResult = LoadUIForm(uiFormsName);
            }
            return baseUIResult;
        }

        /// <summary>
        /// 加载指定名称的 UI窗体
        /// 功能：
        /// 1. 根据“UI窗体名称”，加载预设克隆体。
        /// 2. 根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同的节点。
        /// 3. 隐藏刚创建的UI克隆体。
        /// 4. 把克隆体加入到“所有UI窗体”缓存集合中
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
        private BaseUIForm LoadUIForm(string uiFormName)
        {
            string strUIFormPaths = null;        // UI窗体路径
            GameObject goCloneUIPrefabs = null;  // 创建的UI克隆体预设
            BaseUIForm baseUIForm = null;        // 窗体基类

            // 1. 根据UI窗体名称，得到对应的加载路径
            _DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths);
            // 根据“UI窗体名称”，加载“预设克隆体”
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths, false);
            }

            // 2. 设置“UI克隆体”的父节点(根据克隆体中带的脚本中不同的“位置信息”)
            if(_TraCanvasTransfrom!=null && goCloneUIPrefabs!=null)
            {
                baseUIForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
                if(baseUIForm==null)
                {
                    Debug.Log("baseUIForm==null!, 请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！ 参数uiFormName=" + uiFormName);
                    return null;
                }
                switch (baseUIForm.CurrentUIType.UIForms_Type)
                {
                    case UIFormType.Normal:
                        goCloneUIPrefabs.transform.SetParent(_TraNormal, false);
                        break;
                    case UIFormType.Fixed:
                        goCloneUIPrefabs.transform.SetParent(_TraFixed, false);
                        break;
                    case UIFormType.PopUP:
                        goCloneUIPrefabs.transform.SetParent(_TraPopUp, false);
                        break;
                    default:
                        break;
                }
                // 3. 设置隐藏
                goCloneUIPrefabs.SetActive(false);

                // 4. 把克隆体，加入到“所有UI窗体” （缓存）集合中。
                _DicAllUIForms.Add(uiFormName, baseUIForm);
                return baseUIForm;
            }

            else
            {
                Debug.Log("_TraCanvasTransform==null or goCloneUIPrefabs==null, Please check!, 参数 uiFormName=" + uiFormName);
            }

            Debug.Log("出现不可预估的错误。 参数 uiFormName=" + uiFormName);
            return null;

        }  // Method_end


        /// <summary>
        ///  把当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiFormName">窗体预设的名称</param>
        private void LoadUIToCurrentCache(string uiFormName)
        {
            BaseUIForm baseUiForm;             // UI窗体基类
            BaseUIForm baseUIFormFromAllCache; // 从所有窗体集合中得到窗体

            // 如果“正在显示”的集合中，存在整个UI窗体，则直接返回
            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm != null) return;

            // 把当前窗体，加载到“正在显示”集合中
            _DicAllUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
            if(baseUIFormFromAllCache!=null)
            {
                _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();  // 显示当前窗体
            }

        }

    }  // class_end

}  // namespace_end

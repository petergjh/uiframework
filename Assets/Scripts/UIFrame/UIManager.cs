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
        // 定义“栈”集合，存储显示当前所有“反向切换”的窗体类型
        private Stack<BaseUIForm> _StaCurrentUIForms;

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


        /// <summary>
        /// 初始化核心数据，加载“UI窗体路径”到集合中
        /// </summary>
        public void Awake()
        {
            Debug.Log("开始执行Awake方法，设置核心数据默认值:路径、所有窗体、正在显示的窗体，并提取“UI窗体路径”到集合中");
            // 字段初始化
            _DicFormsPaths = new Dictionary<string, string>();
            _DicAllUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _StaCurrentUIForms = new Stack<BaseUIForm>();
            Debug.LogFormat("1. 字段一: {0},\n字段二：{1},\n字段三：{2},\n字段四{3}\n初始化完成", _DicFormsPaths, _DicAllUIForms, _DicCurrentShowUIForms,_StaCurrentUIForms);

            Debug.LogFormat("2. 初始化加载根UI窗体Canvas预设完成，根UI窗体路径是：{0}", SysDefine.SYS_PATH_CANVAS);
            InitRootCanvasLoading();

            Debug.LogFormat("3. 得到UI根节点:{0}、全屏节点、固定节点、弹出节点、脚本节点", SysDefine.SYS_TAG_CANVAS );
            _TraCanvasTransfrom = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
            //_TraCanvasTransfrom = GameObject.FindGameObjectWithTag("_TagCanvas").transform;
            _TraNormal = _TraCanvasTransfrom.Find("Normal");
            _TraFixed = _TraCanvasTransfrom.Find("Fixed");
            _TraPopUp = _TraCanvasTransfrom.Find("PopUp");
            _TraUIScripts = _TraCanvasTransfrom.Find("_ScriptMgr");

            this.gameObject.transform.SetParent(_TraUIScripts,false);
            Debug.Log("4. 把本脚本做为“根UI窗体”的子节点");

            Debug.Log("5. “根UI窗体”在场景转换时不允许销毁");
            DontDestroyOnLoad(_TraCanvasTransfrom);

            Debug.Log("6. 提取UI“窗体预设”路径: UIPrefabs/LogonUIForm\n Awake初始化完成");
            //先写简单的，后面再用json做配置文件
            if(_DicFormsPaths!=null)
            {
                _DicFormsPaths.Add("LogonUIForm", @"UIPrefabs\LogonUIForm");
                _DicFormsPaths.Add("SelectHeroUIForm", @"UIPrefabs\SelectHeroUIForm");
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

            Debug.LogFormat("一、正在根据UI窗体的名称找到相应预设的路径加载到缓存集合");
            baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
            if (baseUIForms == null) return;

            // 是否清空栈集合中的数据
            if(baseUIForms.CurrentUIType.IsClearStack)
            {
                ClearStackArray();
            }
            
            // 根据定义好的显示模式加载不同的UI窗体
            Debug.LogFormat("二、正在加载显示UI窗体:{0}, UI窗体显示模式是{1}", baseUIForms, baseUIForms.CurrentUIType.UIForms_ShowMode);
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormShowMode.Normal:          // 普通显示窗口模式
                    // 把当前窗体加载到“当前窗体”集合中
                    LoadUIToCurrentCache(uiFormName);
                    Debug.Log("当前窗体已加载到“正在显示”集合中");
                    break;
                case UIFormShowMode.ReverseChange:   // 需要反向切换窗口模式
                    PushUIFormToStack(uiFormName);
                    Debug.Log("当前反向切换窗体已加载到“正在显示”集合中");
                    break;
                case UIFormShowMode.HideOther:       // 隐藏其它窗口模式
                    EnterUIFormsAndHideOther(uiFormName);
                    Debug.Log("隐藏其它窗口");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void CloseUIForms(string uiFormName)
        {
            // 参数检查
            if (string.IsNullOrEmpty(uiFormName)) return;

            BaseUIForm baseUIForm;
            // 所有UI窗体集合中，若没有记录则直接返回
            _DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
                if (baseUIForm == null) return;

            // 根据窗体不同的显示类型，分别按不同的方法关闭
            switch(baseUIForm.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormShowMode.Normal:
                    // 普通窗体的关闭
                    ExitUIForms(uiFormName);
                    Debug.Log("关闭了普通窗体：" + uiFormName);
                    break;

                case UIFormShowMode.ReverseChange:
                    // 反向切换窗体的关闭
                    PopUIForms();
                    break;
                case UIFormShowMode.HideOther:
                    // 隐藏窗体的关闭
                    break;
            }
        }


        #region 显示“UI管理器”内部核心数据，用来测试

        /// <summary>
        /// 显示所有UI窗体的数量
        /// </summary>
        /// <returns></returns>
        public int ShowAllUIFormsCount()
        {
            if (_DicAllUIForms != null)
            {
                return _DicAllUIForms.Count;
            }
            else return 0;
        }

        /// <summary>
        /// 显示“当前窗体”集合中的数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentUIFormsCount()
        {
            if (_DicCurrentShowUIForms != null)
            {
                return _DicCurrentShowUIForms.Count;
            }
            else return 0;
        }

        /// <summary>
        /// 显示“当前栈”集合中窗体数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentStackUIFormsCount()
        {
            if (_StaCurrentUIForms != null)
            {
                return _StaCurrentUIForms.Count;
            }
            else return 0;
        }


        #endregion

        #region 私有方法

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
            Debug.LogFormat("检查UI窗体是否已加载过");
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

            // 0. 根据UI窗体名称，得到对应的加载路径
            _DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths);
            Debug.LogFormat("0. 已经找到UI窗体:{0}的”预设体”路径:{1}", uiFormName, strUIFormPaths);

            // 1. 根据预设体路径，加载“预设克隆体”
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths, false);
                Debug.LogFormat("1. UI窗体:{0}的”预设克隆体“已成功加载", strUIFormPaths);
            }

            // 2. 设置“UI克隆体”的父节点(根据克隆体中带的脚本中不同的“位置信息”)
            if(_TraCanvasTransfrom!=null && goCloneUIPrefabs!=null)
            {
                baseUIForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
                Debug.LogFormat("2. 设置“UI克隆体”的父节点:{0}(根据克隆体中带的脚本中不同的“位置信息”)",uiFormName);

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
                // 3. 设置隐藏克隆体
                goCloneUIPrefabs.SetActive(false);
                Debug.LogFormat("3. 先把克隆体暂时隐藏：{0}", uiFormName);

                // 4. 把克隆体，加入到“所有UI窗体 _DicAllUIForms” （缓存）集合中。
                _DicAllUIForms.Add(uiFormName, baseUIForm);
                Debug.LogFormat("4. 把{0}克隆体，加入到“所有UI窗体{1}” （缓存）集合中. \n 窗体已成功加载到缓存，等待显示中。。。", uiFormName, baseUIForm);
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
        ///  把当前窗体加载到“当前窗体”集合中显示
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

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName"></param>
        private void PushUIFormToStack(string uiFormName)
        {
            // 判断“栈”集合中是否有其它的窗体，有则冻结。
            if(_StaCurrentUIForms.Count>0)
            {
                BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
                // 栈顶元素冻结处理
                topUIForm.Freeze();
            }

            BaseUIForm baseUIForm;
            // 判断UI所有窗体集合是否有指定的UI窗体，有则处理
            _DicAllUIForms.TryGetValue(uiFormName, out baseUIForm);
            if (baseUIForm!=null)
            {
                //当前窗口设置为显示状态
                baseUIForm.Display();

                // 把指定的UI窗体做入栈操作
                _StaCurrentUIForms.Push(baseUIForm);
            }
            else { Debug.Log("baseUIForm==null. Please check! 参数 uiFormName=" + uiFormName); }

        }

        /// <summary>
        /// "普通"属性的窗体的关闭方法
        /// </summary>
        /// <param name="strUIFormName"></param>
        private void ExitUIForms(string strUIFormName)
        {
            BaseUIForm baseUIForm; // 窗体基类
            // "正在显示集合"中如果没有记录，则直接返回。
            _DicCurrentShowUIForms.TryGetValue(strUIFormName, out baseUIForm);
            if (baseUIForm == null) return;

            // 指定窗体，标记为隐藏状态 ， 且从“正在显示集合中移除”
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(strUIFormName);
        }

        /// <summary>
        /// "反向切换“属性的窗体关闭的出栈方法
        /// </summary>
        private void PopUIForms()
        {
            if(_StaCurrentUIForms.Count >=2)
            {
                // 出栈处理
                BaseUIForm topUIForm = _StaCurrentUIForms.Pop();

                //  隐藏处理
                topUIForm.Hiding();

                // 出栈后，下一个窗体“重新显示”
                BaseUIForm nextUIForm = _StaCurrentUIForms.Peek();
                nextUIForm.ReDisplay();
            }
            else if (_StaCurrentUIForms.Count == 1)
            {
                // 出栈处理
                BaseUIForm topUIForms = _StaCurrentUIForms.Pop();

                // 隐藏处理
                topUIForms.Hiding();
            }
        }


        /// <summary>
        /// 打开“隐藏其它”属性的窗体，同时隐藏其它窗口模式
        /// </summary>
        /// <param name="strUIName"></param>
        private void EnterUIFormsAndHideOther(string strUIName)
        {
            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            BaseUIForm baseUIForm;  // UI窗体基类
            _DicCurrentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm != null) return;

            //  把正在显示集合与栈集合中所有的窗体都隐藏
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.Hiding();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.Hiding();
            }

            BaseUIForm BaseUIFormFromAll;  // 从集合中得到的UI窗体基类
            // 把当前窗体加入到”正在显示窗体“”集合中， 且做显示处理
            _DicAllUIForms.TryGetValue(strUIName, out BaseUIFormFromAll);
            if(BaseUIFormFromAll != null)
            {
                _DicCurrentShowUIForms.Add(strUIName, BaseUIFormFromAll);
                BaseUIFormFromAll.Display();
            }
        }

        /// <summary>
        /// 是否清空栈集合中的数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count>=1)
            {
                // 清空栈集合
                _StaCurrentUIForms.Clear();
                return true;
            }
            return false;

        }


        #endregion

    }  // class_end

}  // namespace_end

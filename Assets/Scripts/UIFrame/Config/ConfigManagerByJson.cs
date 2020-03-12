using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UIFrame
{


    /// <summary>
    /// 基于Json配置文件的配置管理器
    /// </summary>
    public class ConfigManagerByJson : IConfigManager
    {
        // 保存键值对应用设置集合
        private static Dictionary<string, string> _AppSetting;

        /// <summary>
        /// 只读属性：得到应用设置（键值对集合）
        /// </summary>
        public Dictionary<string,string> AppSetting
        {
            get { return _AppSetting; }
        }
       // public Dictionary<string, string> AppSetting => throw new System.NotImplementedException();

        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="jsonPath">Json配置文件路径</param>
        public ConfigManagerByJson(string jsonPath)
        {
            _AppSetting = new Dictionary<string, string>();

            // 初始化解析Json数据，加载到集合中
            InitAndAnalysisJson(jsonPath);
        }

        // 方法：得到配置文件AppSetting里的数据条目数量
        public int GetAppSettingMaxNumber()
        {
            if (_AppSetting != null && _AppSetting.Count >= 1)
            {
                return _AppSetting.Count;
            }
            else
            {
                return 0; 
            }
        }

        /// <summary>
        ///  初始化解析Json数据，加载到集合中
        /// </summary>
        /// <param name="jsonPath"></param>
        private void InitAndAnalysisJson(string jsonPath)
        {
            TextAsset configInfo = null;
            KeyValuesInfo keyvalueInfoObj = null;

            // 参数检查
            if (string.IsNullOrEmpty(jsonPath)) return;
            // 解析Json配置文件
            try
            {
                configInfo = Resources.Load<TextAsset>(jsonPath);
                keyvalueInfoObj = JsonUtility.FromJson<KeyValuesInfo>(configInfo.text);
            }
            catch (Exception)
            {
                // 自定义异常
                throw new JsonAnalysisException(GetType()+"/InitAndAnalysisJson()/Json Analysis Exception ! jsonPath="+jsonPath);
            }

            // 数据加载到APPSetting集合中
            foreach(KeyValueNode nodeInfo in keyvalueInfoObj.ConfigInfo)
            {
                _AppSetting.Add(nodeInfo.key, nodeInfo.value);
            }

           

        }


    }



}

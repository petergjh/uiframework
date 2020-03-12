using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UIFrame
{


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
        }

        public int GetAppSettingMaxNumber()
        {
            throw new System.NotImplementedException();
        }

        private void InitAndAnalysisJson(string jsonPath)
        {
            TextAsset configInfo = null;
            KeyValuesInfo keyvalueInfoObj = null;

            // 参数检查
            if (string.IsNullOrEmpty(jsonPath)) return;
            try
            {
                configInfo = Resources.Load<TextAsset>(jsonPath);
            }
            catch { }

           

        }


    }



}

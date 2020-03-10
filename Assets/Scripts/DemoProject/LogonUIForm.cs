using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace DemoProject
{

    public class LogonUIForm : BaseUIForm
    {
        public void Awake()
        {
            // 定义本窗体的性质
            base.CurrentUIType.UIForms_Type = UIFormType.Normal;
            base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
            base.CurrentUIType.UIForms_LucencyType = UIFormLucencyType.Lucency; ;

            // 查找按钮节点
            Transform UILogonForm = GameObject.FindGameObjectWithTag("_TestTagLogonUIForm").transform;
            Transform traLogonSysButton = UILogonForm.Find("BG/Btn_OK");

            // 给按钮注册事件方法
            if(traLogonSysButton != null)
            {
                EventTriggerListener.Get(traLogonSysButton.gameObject).onClick = LogonSys;
            }
        }

        /// <summary>
        /// 登陆方法
        /// </summary>
        public void LogonSys(GameObject go)
        {
            print("登陆方法被执行");
            // 前台或后台检查登陆账号信息
            //  如果成功，切换下一个窗体
            UIManager.GetInstance().ShowUIForms("SelectHeroUIForm");
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace DemoProject
{

    public class MarketUIForm: BaseUIForm
    {
        private void Awake()
        {
            // 窗体性质
            CurrentUIType.UIForms_Type = UIFormType.PopUP;  // 窗体位置类型：弹出窗体
            CurrentUIType.UIForms_LucencyType = UIFormLucencyType.Translucence;  // 透明度类型：半透明度 
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange; // 窗体显示类型：反向切换
            Debug.Log("已设置窗体性质。");

            // 注册按钮事件
            RigisterButtonObjectEvent("Btn_Close",
                p=> CloseUIForm()
                );

        }

    }

}

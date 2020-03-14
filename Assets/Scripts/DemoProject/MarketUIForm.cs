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
            Debug.Log("游戏商城窗体MarketUIForm开始初始化");
            // 窗体性质
            CurrentUIType.UIForms_Type = UIFormType.PopUP;  // 窗体位置类型：弹出窗体
            CurrentUIType.UIForms_LucencyType = UIFormLucencyType.Lucency;  // 透明度类型：半透明度 
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange; // 窗体显示类型：反向切换
            Debug.Log("已设置窗体性质。");

            // 注册按钮事件
            RigisterButtonObjectEvent("Btn_Close",
                p=> CloseUIForm()
                );

        }

    }

}

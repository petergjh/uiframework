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
            CurrentUIType.UIForms_Type = UIFormType.PopUP;  // 弹出窗体
            CurrentUIType.UIForms_LucencyType = UIFormLucencyType.Pentrate;  // 半透明度 
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
            Debug.Log("已设置窗体性质。");

            // 按钮事件
            //RigisterButtonObjectEvent("")

        }

    }

}

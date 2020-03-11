using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace DemoProject
{

    public class MainCityUIForm : BaseUIForm
    {
        private void Awake()
        {
            // 窗体性质使用默认值
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;

            // 事件注册
            RigisterButtonObjectEvent("BtnMarket",
                p => OpenUIForm("MarketUIForm"));
            Debug.Log("注册BtnMarket点击事件，打开MarketUIForm窗体");
        }

    }

}

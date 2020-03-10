using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace DemoProject
{

    public class SelectHeroUIForm: BaseUIForm
    {
        public void Awake()
        {
            // 窗体的性质
            // CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
            Debug.Log("显示当前窗体时隐藏其它窗体,减少不必要加载的性能消耗");

            // 注册进入主城的事件
            RigisterButtonObjectEvent("BtnConfirm", EnterMainCityUIForm);

            // 注册返回窗体的事件
            RigisterButtonObjectEvent("BtnClose", ReturnLogonUIForm);
            // 可用Lambda表达式简写 
            // RigisterButtonObjectEvent("BtnClose", m => CloseUIForm());

        }

        public void Start()
        {
            // 显示“UI管理器”内部的状态
            print("所有窗体集合中的数量 = " + UIManager.GetInstance().ShowAllUIFormsCount());
            print("当前窗体集合中的数量 = " + UIManager.GetInstance().ShowCurrentUIFormsCount());
            print("栈窗体集合中的数量 = " + UIManager.GetInstance().ShowCurrentStackUIFormsCount());

        }

        public void EnterMainCityUIForm(GameObject go)
        {
            print("进入主城UI窗体！");
        }

        public void ReturnLogonUIForm(GameObject go)
        {
            //UIManager.GetInstance().CloseUIForms("SelectHeroUIForm");
            //UIManager.GetInstance().CloseUIForms(ProConst.SELECT_HERO_FORM);
            CloseUIForm();

            print("返回");
            print(GetType());  // 返回命名空间和类名
        }

    }

}

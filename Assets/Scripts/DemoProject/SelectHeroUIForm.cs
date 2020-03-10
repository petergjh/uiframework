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
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
            Debug.Log("显示当前窗体时隐藏其它窗体,减少不必要加载的性能消耗");

            // 注册事件

        }

        public void Start()
        {
            // 显示“UI管理器”内部的状态
            print("所有窗体集合中的数量 = " + UIManager.GetInstance().ShowAllUIFormsCount());
            print("当前窗体集合中的数量 = " + UIManager.GetInstance().ShowCurrentUIFormsCount());
            print("栈窗体集合中的数量 = " + UIManager.GetInstance().ShowCurrentStackUIFormsCount());

        }

    }

}

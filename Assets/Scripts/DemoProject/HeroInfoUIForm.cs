using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace DemoProject
{

    /// <summary>
    /// 英雄信息显示
    /// </summary>
    public class HeroInfoUIForm : BaseUIForm
    {
        private void Awake()
        {

            // 定义窗体性质 
            CurrentUIType.UIForms_Type = UIFormType.Fixed;  // 把此窗体挂到相应unity的父节点上显示

            
        }

    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace DemoProject
{
    public class StartProject : MonoBehaviour
    {
        void Start()
        {
            // 加载登陆窗体
            UIManager.GetInstance().ShowUIForms("LogonUIForm");
            print("登陆窗体 LogonUIForm 加载已完成！");

        }
    }

}
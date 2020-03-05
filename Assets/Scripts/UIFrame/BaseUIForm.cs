using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI窗体父类
/// 定义窗体的四个生命周期
/// 1. Display 显示状态
/// 2. Hiding 隐藏状态
/// 3. ReDisplay 再显示状态
/// 4. Freeze 冻结状态
/// </summary>

namespace UIFrame
{

    public class BaseUIForm : MonoBehaviour
    {
        //定义一个UI窗体类型的私有字段
        private UIType _CurrentUIType = new UIType();

        // 定义UI窗体类型的公共属性
        public UIType CurrentUIType { get => _CurrentUIType; set => _CurrentUIType = value; }
        
        // 定义四个虚方法，设置窗体的四个状态
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void ReDisplay()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }
    }

}

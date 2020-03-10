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

        #region 窗体的四种状态

        // 定义四个虚方法，设置窗体显示的四个状态
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

        #endregion


        #region 封装子类常用的方法

        /// <summary>
        /// 把注册按钮事件利用委托进行封装
        /// </summary>
        /// <param name="buttonName">节点名称</param>
        /// <param name="delHandle">委托事件，需注册的方法</param>
        protected void RigisterButtonObjectEvent(string buttonName, EventTriggerListener.VoidDelegate delHandle)
        {
            GameObject goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
            Debug.Log("已查找到子节点: " + goButton);

            // 给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton.gameObject).onClick = delHandle;
            }
        }
        #endregion


    }


}

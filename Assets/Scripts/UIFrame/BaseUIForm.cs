using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI窗体父类
/// 定义窗体的四个生命周期
/// 1. Enter 显示状态
/// 2. Pause 暂停状态
/// 3. Resume 继续状态
/// 4. End 退出状态
/// </summary>

namespace UIFrame
{

    public class BaseUIForm : MonoBehaviour
    {
        //定义一个UI窗体类型的私有字段
        private UIType _CurrentUIType = new UIType();

        // 定义UI窗体类型的公共属性
        public UIType CurrentUIType { get => _CurrentUIType; set => _CurrentUIType = value; }

        public virtual void Enter()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void Pause()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void Resume()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void End()
        {
            this.gameObject.SetActive(true);
        }
    }

}

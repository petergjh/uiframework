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

            // 注册道具事件：神杖
            RigisterButtonObjectEvent("BtnTicket",
                p =>
                {
                    // 打开子窗体
                    OpenUIForm("PropDetailUIForm");
                    // 传值
                    // KeyValueUpdate kvs = new KeyValueUpdate("ticket", "神杖道具详情:");
                    // MessageCenter.SendMessage("Props", kvs);
                    // 发送方法进行了封装重构
                    SendMessage("Props","ticket", "神杖道具详情:");
                }
                );

            // 注册道具事件：战靴
            RigisterButtonObjectEvent("BtnShoe",
                 p =>
                {
                    // 打开子窗体
                    OpenUIForm("PropDetailUIForm");
                    // 传值
                    KeyValueUpdate kvs = new KeyValueUpdate("shoes", "战靴道具详情:");
                    MessageCenter.SendMessage("Props", kvs);
                }
                );

            // 注册道具事件：盔甲
            RigisterButtonObjectEvent("BtnCloth",
                 p =>
                {
                    // 打开子窗体
                    OpenUIForm("PropDetailUIForm");
                    // 传值
                    KeyValueUpdate kvs = new KeyValueUpdate("cloth", "盔甲道具详情:");
                    MessageCenter.SendMessage("Props", kvs);
                }
                );


        }

    }

}

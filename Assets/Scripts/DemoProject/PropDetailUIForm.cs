using System.Collections;
using System.Collections.Generic;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject
{
    public class PropDetailUIForm : BaseUIForm
    {
        public Text TxtName;  // 窗体名称
        private void Awake()
        {
            // 窗体的性质
            CurrentUIType.UIForms_Type = UIFormType.PopUP;
            CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
            CurrentUIType.UIForms_LucencyType = UIFormLucencyType.Translucence;

            // 按钮的注册
            // BtnClose关闭按钮
            RigisterButtonObjectEvent("BtnClose",
                p =>
                {
                    CloseUIForm();
                }
                );

            // 接受消息
            //           MessageCenter.AddMsgListener("Props",
            //              p =>
            //             {
            //                Debug.Log("接受消息." );
            //                   if (p.Key.Equals("ticket"))
            //                   {
            //                   if (TxtName)
            //                  {
            //                     TxtName.text = p.Values.ToString();

            //                   }
            //
            //                    else if (p.Key.Equals("shoes"))
            //                    {
            //                        if (TxtName)
            //                        {
            //                            TxtName.text = p.Values.ToString();
            //                        }
            //                    }
            //
            //                    else if (p.Key.Equals("cloth"))
            //                    {
            //                        if (TxtName)
            //                        {
            //                            TxtName.text = p.Values.ToString();
            //                        }
            //                    }
            //            });


            ReceiveMessage("Props", 
                p =>
                {
                    if (TxtName)
                    {
                        TxtName.text = p.Values.ToString();
                    }
                    Debug.Log("已接收到消息：" + p.Key + p.Values);
                });

        } // Awake_end

    }
    




}


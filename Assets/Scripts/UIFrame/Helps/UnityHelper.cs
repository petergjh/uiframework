using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UIFrame
{

    /// <summary>
    /// 定义帮助类
    /// </summary>
    public class UnityHelper: MonoBehaviour
    {
         
        /// <summary>
        /// 递归查找子节点方法
        /// </summary>
        /// <param name="goParent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindTheChildNode(GameObject goParent,string childName)
        {
            Transform searchTrans = null;  // 查找结果
            searchTrans = goParent.transform.Find(childName);
            if (searchTrans==null)
            {
                Debug.Log("开始递归查找子节点: "+childName);
                foreach(Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, childName);
                    if(searchTrans != null)
                    {
                        return searchTrans;
                    }
                }
            }
            return searchTrans;
        }


        /// <summary>
        /// 获取子节点脚本
        /// </summary>
        /// <typeparam name="T">泛型，这里代指unity组件</typeparam>
        /// <param name="goParent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static T GetTheChildNodeComponentScripts<T>(GameObject goParent, string childName) where T:Component
        {
            Transform searchTransformNode = null;
            searchTransformNode = FindTheChildNode(goParent, childName);
            if(searchTransformNode != null)
            {
                Debug.Log(" 查找并返回子节点脚本：" + childName);
                return searchTransformNode.gameObject.GetComponent<T>();
            }
            else
            {
                return null;
            }
        }

        //给子节点添加脚本
        public static T AddChildNodeComponent<T>(GameObject goParent, string childName) where T:Component
        {
            //  查找特定子节点

            // 如果查找成功，比较若有同名重复脚本则删除，无测添加

            // 如果查找不成功，返回null

        }



    }

}
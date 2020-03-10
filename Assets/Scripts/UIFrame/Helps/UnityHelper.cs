using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UIFrame
{

    public class UnityHelper: MonoBehaviour
    {
         
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
    }

}
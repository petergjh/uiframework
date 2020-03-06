using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{

    public class Test : MonoBehaviour
    {
        Dictionary<string, string> _dicTest = new Dictionary<string, string>();

        private void Start()
        {
            string strResult = string.Empty; // 输出内容

            _dicTest.Add("zhangsan", "张三");
            _dicTest.Add("lisi", "李四");

            //查询
            _dicTest.TryGetValue("zhangsan", out strResult);
            print("查询结果 strResult=" + strResult);

        }
    }


}

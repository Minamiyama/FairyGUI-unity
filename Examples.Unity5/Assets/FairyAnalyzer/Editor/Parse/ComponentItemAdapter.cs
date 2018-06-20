// // ================================================================
// // FileName:ComponentItem.cs
// // User: Baron
// // CreateTime:2018/6/20
// // Description: 组件信息的中间件
// // ================================================================
using UnityEngine.UI;

namespace FairyAnalyzer
{
    public class ComponentItemAdapter : IComponnetItem
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// 字段的索引
        /// </summary>
        public int FieldIndex { get; set; }

        /// <summary>
        /// 字段名字
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 获取字段定义
        /// </summary>
        /// <returns></returns>
        public string GetFeildDefine()
        {
            return string.Format("public {0} m_{1};", FieldType, FieldName);
        }

        /// <summary>
        /// get时的描述
        /// </summary>
        /// <returns></returns>
        public string GetFieldGetMethod()
        {
            if (FieldType == "Controller")
            {
                return string.Format("m_{0} = GetControllerAt({1});", FieldName, FieldIndex);
            }
            else if (FieldType == "Transition")
            {
                return string.Format("m_{0} = GetTransitionAt({1});", FieldName, FieldIndex);
            }
            else
            {
                return string.Format("m_{0} = GetChildAt({1});", FieldName, FieldIndex);
            }
        }
    }
}
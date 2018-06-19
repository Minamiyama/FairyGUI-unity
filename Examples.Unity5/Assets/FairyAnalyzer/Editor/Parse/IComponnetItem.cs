// // ================================================================
// // FileName:IComponnetItem.cs
// // User: Baron
// // CreateTime:2018/6/20
// // Description: 组件接口
// // ================================================================

namespace FairyAnalyzer
{
    public interface IComponnetItem
    {
        /// <summary>
        /// 字段的索引
        /// </summary>
        int FieldIndex { get; set; }
        
        /// <summary>
        /// 字段名字
        /// </summary>
        string FieldName { get; set; }
        
        /// <summary>
        /// 字段类型
        /// </summary>
        string FieldType { get; set; }
        
        /// <summary>
        /// 获取字段定义描述
        /// </summary>
        /// <returns></returns>
        string GetFeildDefine();

        /// <summary>
        /// Get方法里面的描述返回
        /// </summary>
        /// <returns></returns>
        string GetFieldGetMethod();
    }
}
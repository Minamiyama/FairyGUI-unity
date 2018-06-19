using System.Collections.Generic;

namespace FairyAnalyzer
{
    public class ComponentAdapter
    {
        /// <summary>
        /// 扩展类型
        /// </summary>
        public string Extention;

        /// <summary>
        /// item 组件
        /// </summary>
        public List<ComponentItemAdapter> ItemsAdapters = new List<ComponentItemAdapter>();
        
    }
}
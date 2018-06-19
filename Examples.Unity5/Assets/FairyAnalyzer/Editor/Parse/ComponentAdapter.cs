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
        public List<ComponentItemAdapter> ComponnetItems = new List<ComponentItemAdapter>();

        /// <summary>
        /// controller队列
        /// </summary>
        public List<ComponentItemAdapter> ControllerItems = new List<ComponentItemAdapter>();
        
        /// <summary>
        /// transition队列
        /// </summary>
        public List<ComponentItemAdapter> TransitionItems = new List<ComponentItemAdapter>();
        
    }
}
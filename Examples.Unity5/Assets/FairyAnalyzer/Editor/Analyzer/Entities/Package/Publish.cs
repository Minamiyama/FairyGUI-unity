using System.Xml.Serialization;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// 发布设置
    /// </summary>
    public class Publish
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 是否输出代码
        /// </summary>
        [XmlAttribute("genCode")]
        public bool GenCode { get; set; }
    }
}
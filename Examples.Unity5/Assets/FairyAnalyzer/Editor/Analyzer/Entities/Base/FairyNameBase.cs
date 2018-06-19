using System.Xml.Serialization;

namespace FairyAnalyzer.Base
{
    public abstract class FairyNameBase : FairyIdBase
    {
        [FairyProperty]
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }
}
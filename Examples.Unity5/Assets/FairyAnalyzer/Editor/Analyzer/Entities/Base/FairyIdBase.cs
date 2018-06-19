using System.Xml.Serialization;

namespace FairyAnalyzer.Base
{
    public abstract class FairyIdBase : FairyBase
    {
        [FairyProperty]
        [XmlAttribute(AttributeName = "id")]
        public string ID { get; set; }
    }
}
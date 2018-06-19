using System.Xml.Serialization;

namespace FairyAnalyzer.Package
{
    public class Publish
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
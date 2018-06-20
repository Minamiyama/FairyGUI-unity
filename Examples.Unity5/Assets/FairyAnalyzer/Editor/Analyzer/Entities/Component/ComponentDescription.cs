using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FairyAnalyzer.Component
{
    [XmlRoot("component")]
    public class ComponentDescription
    {
        [XmlElement("Button", typeof(Button)),
         XmlElement("ComboBox", typeof(ComboBox))]
        public Component ComponentType { get; set; }

        [XmlElement("controller")]
        public List<Controller> Controllers { get; set; }

        [XmlArray(ElementName = "displayList")]
        public List<Component> DisplayList { get; set; }

        public static ComponentDescription Parse(string xmlPath)
        {
            FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(ComponentDescription));
            var p = (ComponentDescription)xs.Deserialize(fs);
            return p;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FairyAnalyzer.Component
{
    [XmlRoot("component")]
    public class ComponentDescription
    {
        [XmlElement("Button", typeof(Button))]
        [XmlElement("Slider", typeof(Slider))]
        [XmlElement("ProgressBar", typeof(ProgressBar))]
        [XmlElement("ScrollBar", typeof(ScrollBar))]
        [XmlElement("ComboBox", typeof(ComboBox))]
        [XmlElement("Label", typeof(Label))]
        public ExtentionType ComponentType { get; set; }

        [XmlElement("controller")]
        public List<Controller> Controllers { get; set; }

        [XmlArrayItem("component", typeof(CustomComponent))]
        [XmlArrayItem("group", typeof(Group))]
        [XmlArrayItem("graph", typeof(Graph))]
        [XmlArrayItem("list", typeof(List))]
        [XmlArrayItem("loader", typeof(Loader))]
        [XmlArrayItem("image", typeof(Image))]
        [XmlArrayItem("richtext", typeof(RichText))]
        [XmlArrayItem("text", typeof(Text))]
        [XmlArray(ElementName = "displayList")]
        public List<ComponentType> DisplayList { get; set; }

        public static ComponentDescription Parse(string xmlPath)
        {
            FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(ComponentDescription));
            var p = (ComponentDescription)xs.Deserialize(fs);
            return p;
        }
    }
}
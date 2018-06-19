using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using FairyAnalyzer.Base;

namespace FairyAnalyzer.Component
{
    [XmlRoot("Component")]
    public class ComponentDescription
    {
        public Controller Controller { get; set; }

        [XmlArray(ElementName = "displayList")]
        public List<Component> DisplayList { get; set; }
    }

    [Serializable]
    public class Controller : FairyNameBase
    {
        [XmlElement]
        public string pages;

        [XmlIgnore]
        public List<string> Pages
        {
            get { return pages.Split(',').ToList(); }
        }
    }

    [XmlInclude(typeof(Text))]
    [Serializable]
    public class Component : FairyNameBase
    {
        [XmlAttribute("src")]
        public string Src { get; set; }
    }

    [Serializable]
    public class Group : Component { }

    [Serializable]
    public class Text : Component { }
}
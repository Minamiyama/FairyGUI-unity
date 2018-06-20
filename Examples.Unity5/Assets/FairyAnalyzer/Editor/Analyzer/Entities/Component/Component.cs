using System;
using System.Diagnostics;
using System.Xml.Serialization;
using FairyAnalyzer.Base;

namespace FairyAnalyzer.Component
{
    [XmlInclude(typeof(Text))]
    [Serializable]
    public class Component : FairyNameBase
    {
        [XmlAttribute("src")]
        public string Src { get; set; }
    }

    [Serializable]
    public class Text : Component
    {
        [XmlAttribute("input")]
        public bool Input { get; set; }
    }

    [Serializable]
    public class Image : Component { }

    [Serializable]
    public class Loader : Component { }

    [Serializable]
    public class List : Component { }

    [Serializable]
    public class Graph : Component { }

    [Serializable]
    public class Group : Component { }

}
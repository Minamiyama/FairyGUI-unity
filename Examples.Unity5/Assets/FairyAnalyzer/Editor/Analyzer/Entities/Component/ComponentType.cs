﻿using System;
using System.Diagnostics;
using System.Xml.Serialization;
using FairyAnalyzer.Base;

namespace FairyAnalyzer.Component
{
    [XmlInclude(typeof(CustomComponent))]
    [XmlInclude(typeof(Group))]
    [XmlInclude(typeof(Graph))]
    [XmlInclude(typeof(List))]
    [XmlInclude(typeof(Loader))]
    [XmlInclude(typeof(Image))]
    [XmlInclude(typeof(RichText))]
    [XmlInclude(typeof(Text))]
    [Serializable]
    public abstract class ComponentType : FairyNameBase
    {

    }

    [Serializable]
    public class CustomComponent : ComponentType
    {
        [XmlAttribute("src")]
        public string Src { get; set; }
    }

    [Serializable]
    public class Text : ComponentType
    {
        [XmlAttribute("input")]
        public bool Input { get; set; }
    }

    [Serializable]
    public class RichText : ComponentType { }

    [Serializable]
    public class Image : ComponentType { }

    [Serializable]
    public class Loader : ComponentType { }

    [Serializable]
    public class List : ComponentType { }

    [Serializable]
    public class Graph : ComponentType { }

    [Serializable]
    public class Group : ComponentType
    {
        [XmlAttribute("advanced")]
        public bool Advanced { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using FairyAnalyzer.Base;

namespace FairyAnalyzer.Component
{
    [Serializable]
    public class Controller : FairyNameBase
    {
        [XmlAttribute] public string pages;

        [XmlIgnore]
        public List<string> Pages
        {
            get { return pages.Split(',').ToList(); }
        }
    }
}
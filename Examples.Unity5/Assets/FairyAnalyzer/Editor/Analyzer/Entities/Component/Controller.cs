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

        private List<string> _pages;
        [XmlIgnore]
        public List<string> Pages
        {
            get
            {
                if (_pages == null)
                {
                    _pages = new List<string>();
                    var split = pages.Split(',');
                    for (var i = 0; i < split.Length / 2; i++)
                    {
                        _pages.Add(split[i * 2 + 1]);
                    }
                }

                return _pages;
            }
        }
    }
}
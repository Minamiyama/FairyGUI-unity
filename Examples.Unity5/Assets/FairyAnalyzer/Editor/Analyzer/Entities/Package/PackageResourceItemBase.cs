using System;
using System.Xml.Serialization;
using FairyAnalyzer.Base;

namespace FairyAnalyzer.Package
{
    [XmlInclude(typeof(Swf))]
    [XmlInclude(typeof(Sound))]
    [XmlInclude(typeof(MovieClip))]
    [XmlInclude(typeof(Component))]
    [XmlInclude(typeof(Image))]
    [Serializable]
    public class PackageResourceItemBase : FairyNameBase
    {
        [FairyProperty]
        [XmlAttribute("path")]
        public string Path { get; set; }

        [FairyProperty]
        [XmlAttribute("exported")]
        public bool Exported { get; set; }
    }

    [Serializable]
    public class Image : PackageResourceItemBase { }
    [Serializable]
    public class Component : PackageResourceItemBase { }
    [Serializable]
    public class MovieClip : PackageResourceItemBase { }
    [Serializable]
    public class Sound : PackageResourceItemBase { }
    [Serializable]
    public class Swf : PackageResourceItemBase { }
    public class Font : PackageResourceItemBase { }
}
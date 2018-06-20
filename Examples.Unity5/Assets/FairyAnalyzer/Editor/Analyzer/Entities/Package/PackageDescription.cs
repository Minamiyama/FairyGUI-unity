using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using FairyAnalyzer.Base;

namespace FairyAnalyzer.Package
{
    [XmlRoot("packageDescription")]
    public class PackageDescription : FairyIdBase
    {
        [XmlArrayItem("image", typeof(Image)),
         XmlArrayItem("component", typeof(Component)),
         XmlArrayItem("movieclip", typeof(MovieClip)),
         XmlArrayItem("sound", typeof(Sound)),
         XmlArrayItem("swf", typeof(Swf)),
         XmlArrayItem("font", typeof(Font))]
        [XmlArray(ElementName = "resources")]
        public List<PackageResourceItemBase> Resources { get; set; }
        [XmlElement(ElementName = "publish")]
        public Publish Publish { get; set; }

        public static PackageDescription Parse(string packagePath)
        {
            var descPath = Path.Combine(packagePath, "package.xml");

            FileStream fs = new FileStream(descPath, FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(PackageDescription));
            var p = (PackageDescription)xs.Deserialize(fs);

            return p;
        }
    }
}

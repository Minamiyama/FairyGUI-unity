using System;
using System.Xml.Linq;

namespace FairyAnalyzer.Base
{
    public abstract class FairyBase
    {
        //        public FairyBase ReadFromXML(XElement xml)
        //        {
        //            var properties = GetType().GetProperties();
        //
        //            foreach (var property in properties)
        //            {
        //                var attributes = (FairyPropertyAttribute[])property.GetCustomAttributes(typeof(FairyPropertyAttribute), true);
        //                if (attributes != null && attributes.Length > 0) //读取XML属性
        //                {
        //                    var attr = attributes[0];
        //                    var attributeName = property.Name.ToLowerInvariant();
        //                    if (string.IsNullOrEmpty(attr.Name) == false)
        //                    {
        //                        attributeName = attr.Name;
        //                    }
        //
        //                    var xmlAttr = xml.Attribute(attributeName);
        //                    if (xmlAttr!=null)
        //                    {
        //                        if (property.PropertyType == typeof(bool))
        //                        {
        //                            property.SetValue(this, Convert.ToBoolean(xml.Attribute(attributeName).Value), null);
        //                        }
        //                        else if (property.PropertyType == typeof(string))
        //                        {
        //                            property.SetValue(this, xml.Attribute(attributeName).Value, null);
        //                        }
        //                    }
        //                }
        //                else //读取XML节点
        //                {
        //
        //                }
        //            }
        //
        //            foreach (var property in properties)
        //            {
        //
        //            }
        //        }
    }
}
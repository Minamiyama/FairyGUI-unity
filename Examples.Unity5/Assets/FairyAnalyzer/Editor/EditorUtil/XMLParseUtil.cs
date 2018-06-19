// // ================================================================
// // FileName:XMLParse.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: 解析xml相关
// // ================================================================

using System.Collections.Generic;
using System.Xml;

namespace FairyAnalyzer
{
    public class XMLParseUtil
    {
        /// <summary>
        /// 根据package path路径，获取所有的组件
        /// </summary>
        /// <param name="_packagePath"></param>
        /// <returns></returns>
        public static List<FairyGUIComponentInfo> GetAllComponentByPackagePath(string _packagePath)
        {
            if (true == string.IsNullOrEmpty(_packagePath))
            {
                return null;
            }

            List<FairyGUIComponentInfo> componentsList = new List<FairyGUIComponentInfo>();
            XmlDocument  doc            = new XmlDocument ();
            doc.Load (_packagePath);
            XmlNode root          = doc.SelectSingleNode ("packageDescription");
            XmlNode resourcesNode = root.SelectSingleNode("resources");
            if (null != resourcesNode)
            {
                foreach (XmlElement xmlElement in resourcesNode)
                {
                    if (xmlElement.Name == "component")
                    {
                        var fairyComponentInfo = new FairyGUIComponentInfo();
                        var componentAttribute = xmlElement.Attributes["name"];
                        if (null != componentAttribute)
                        {
                            fairyComponentInfo.ComponnetName = componentAttribute.InnerText;
                        }

                        var isExportedAttribute = xmlElement.Attributes["exported"];
                        if (null != isExportedAttribute)
                        {
                            fairyComponentInfo.isFairyIsExport = isExportedAttribute.InnerText == "true";
                        }
                        componentsList.Add(fairyComponentInfo);
                    }
                }
            }
            return componentsList;
        }
    }
}
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
        public static List<FairyGUIComponentInfo> GetAllComponentByPackagePath(string _packageName, string _packagePath)
        {
            if (true == string.IsNullOrEmpty(_packagePath))
            {
                return null;
            }

            List<FairyGUIComponentInfo> componentsList = new List<FairyGUIComponentInfo>();
            XmlDocument                 doc            = new XmlDocument ();
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
                            fairyComponentInfo.ComponentName = componentAttribute.InnerText;
                            fairyComponentInfo.PackageName   = _packageName;
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


        /// <summary>
        /// 根据解析component xml里面的具体内容
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static ComponentAdapter GetComponnetsInfoByPath(string _path)
        {
            if (true == string.IsNullOrEmpty(_path))
            {
                return null;
            }

            ComponentAdapter componentAdapter = new ComponentAdapter();
            XmlDocument      doc              = new XmlDocument ();
            doc.Load (_path);
            XmlNode root      = doc.SelectSingleNode ("component");
            var     extention = root.Attributes["extention"];
            if (null != extention)
            {
                componentAdapter.Extention = string.Format("G{0}", extention.InnerText);
            }

            // 解析组件
            XmlNode displayList = root.SelectSingleNode("displayList");
            if (null != displayList)
            {
                int componentIndex = 0;
                foreach (XmlElement element in displayList)
                {
                    var    item = new ComponentItemAdapter();
                    string type = "G" + element.Name.Substring(0, 1).ToUpper() + element.Name.Substring(1);
                    item.FieldType = type;
                    var nameAttribute = element.Attributes["name"];
                    if (null != nameAttribute)
                    {
                        item.FieldName = nameAttribute.InnerText;
                    }
                    item.Id = element.Attributes["id"].InnerText;
                    item.FieldIndex = componentIndex;
                    componentAdapter.ComponnetItems.Add(item);
                    componentIndex++;
                }
            }

            // 解析控制器
            XmlNode controllerList = root.SelectSingleNode("controller");
            if (null != controllerList)
            {
                int controllerIndex = 0;
                foreach (XmlElement xmlElement in controllerList)
                {
                    var controllerItem = new ComponentItemAdapter();
                    controllerItem.FieldName = xmlElement.Attributes["name"].InnerText;
                    controllerItem.FieldType = "Controller";
                    controllerItem.FieldIndex = controllerIndex;
                    componentAdapter.ControllerItems.Add(controllerItem);
                    controllerIndex++;
                }
            }
            
            // 解析动效
            XmlNode transitionList = root.SelectSingleNode("transition");
            if (null != transitionList)
            {
                int transitionIndex = 0;
                foreach (XmlElement xmlElement in transitionList)
                {
                    var transitionItem = new ComponentItemAdapter();
                    transitionItem.FieldName  = xmlElement.Attributes["name"].InnerText;
                    transitionItem.FieldType  = "Transition";
                    transitionItem.FieldIndex = transitionIndex;
                    componentAdapter.TransitionItems.Add(transitionItem);
                    transitionIndex++;
                }
            }
            return componentAdapter;
        }
    }
}
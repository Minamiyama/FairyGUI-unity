using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FairyAnalyzer.Component;
using UnityEngine;
using UnityScript.Steps;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// Fairy资产
    /// </summary>
    public class FairyAsset
    {
        public Dictionary<string, FairyPackage> Packages;
        public Setting.Publish Publish;

        public Dictionary<string, Dictionary<string, FairyComponent>> Components;

        public FairyAsset(string guiProjectRootPath)
        {
            if (Path.IsPathRooted(guiProjectRootPath)) //绝对路径
            {

            }
            else //相对路径
            {

            }

            var files = Directory.GetFiles(guiProjectRootPath);
            bool isGuiProject = false;
            foreach (var file in files)
            {
                if (Path.GetExtension(file) == ".fairy")
                {
                    isGuiProject = true;
                    break;
                }
            }

            if (false == isGuiProject)
            {
                throw new Exception("不是FairyGUI项目目录");
            }

            var packagePathes = Directory.GetDirectories(string.Format("{0}/assets", guiProjectRootPath));
            var publishJson = File.ReadAllText(string.Format("{0}/settings/Publish.json", guiProjectRootPath));
            Publish = LitJson.JsonMapper.ToObject<Setting.Publish>(publishJson);

            Packages = new Dictionary<string, FairyPackage>();
            Components = new Dictionary<string, Dictionary<string, FairyComponent>>();
            foreach (var packagePath in packagePathes)
            {
                var package = new FairyPackage(packagePath);
                Packages[package.PackageID] = package;

                Components[package.PackageID] = new Dictionary<string, FairyComponent>();

                foreach (var resKey in package.Components.Keys)
                {
                    var comp = package.Components[resKey];
                    Components[comp.PackageID][comp.ComponentID] = comp;
                }
            }
        }

        public void Output(string outputPath)
        {
            var outputList = new List<FairyComponent>();
            var dealed = new Dictionary<string, Dictionary<string, FairyComponent>>();
            foreach (var pkgKey in Components.Keys)
            {
                Debug.Log(string.Format("[Package]********************************{0}", Packages[pkgKey].PackageDescription.Publish.Name));
                foreach (var compKey in Components[pkgKey].Keys)
                {
                    var comp = Components[pkgKey][compKey];
                    if (comp.ResoureInfo.Exported)
                    {
                        outputList.Add(comp);
                        Debug.Log(string.Format("[Component]{0}", comp.ResoureInfo.Name));
                    }
                }
            }

            for (var currentIndex = 0; currentIndex < outputList.Count; currentIndex++)
            {
                var currComp = outputList[currentIndex];
                if (dealed.ContainsKey(currComp.PackageID) && dealed[currComp.PackageID].ContainsKey(currComp.ComponentID))
                {
                    continue;
                }

                if (dealed.ContainsKey(currComp.PackageID) == false)
                {
                    dealed[currComp.PackageID] = new Dictionary<string, FairyComponent>();
                }

                dealed[currComp.PackageID][currComp.ComponentID] = currComp;

                foreach (var componentType in currComp.ComponentDescription.DisplayList)
                {
                    var customComponent = componentType as CustomComponent;
                    if (customComponent != null)
                    {
                        if (IsDefaultName(customComponent.Name))
                        {
                            if (Publish.codeGeneration.ignoreNoname)
                            {
                                continue;
                            }
                        }

                        var pkgId = currComp.PackageID;
                        if (string.IsNullOrEmpty(customComponent.Pkg) == false)
                        {
                            Debug.LogWarning(string.Format("跨包: {0}-{1}引用{2}-{3}", currComp.PackageID, currComp.ComponentID, customComponent.Pkg, customComponent.Src));
                            pkgId = customComponent.Pkg;
                        }

                        outputList.Add(Components[pkgId][customComponent.Src]);
                    }
                }
            }

            //开始处理输出
            var classTemplate = Resources.Load<TextAsset>("Component.template");
            Debug.Log(classTemplate.text);
            foreach (var pkgKey in dealed.Keys)
            {
                var package = Packages[pkgKey];
                var packageName = package.PackageDescription.Publish.Name;
                var fairyPackageName = packageName;
                if (string.IsNullOrEmpty(Publish.codeGeneration.packageName) == false)
                {
                    packageName = string.Format("{0}.{1}", Publish.codeGeneration.packageName, packageName);
                }

                var pkgOutputPath = string.Format("{0}/{1}", outputPath, package.PackageDescription.Publish.Name);
                if (Directory.Exists(pkgOutputPath))
                {
                    Directory.Delete(pkgOutputPath, true);
                }
                Directory.CreateDirectory(pkgOutputPath);

                foreach (var compKey in dealed[pkgKey].Keys)
                {
                    var comp = dealed[pkgKey][compKey];

                    var fairyComponentName = comp.ResoureInfo.Name.Replace(".xml", "");
                    string className = GetClassName(comp);

                    var filePath = string.Format("{0}/{1}.cs", pkgOutputPath, className);
                    var str = classTemplate.text;

                    var componentName = "GComponent";
                    if (string.IsNullOrEmpty(comp.ComponentDescription.Extention) == false)
                    {
                        componentName = string.Format("G{0}", comp.ComponentDescription.Extention);
                    }

                    var uiPath = string.Format("ui://{0}{1}", pkgKey, compKey);

                    var createInstance = string.Format("{3,12}return ({0})UIPackage.CreateObject(\"{1}\", \"{2}\");", className, fairyPackageName, fairyComponentName, " ");

                    var variable = new StringBuilder();
                    var content = new StringBuilder();

                    for (var i = 0; i < comp.ComponentDescription.Controllers.Count; i++)
                    {
                        var con = comp.ComponentDescription.Controllers[i];
                        variable.AppendLine(string.Format("{2,8}public Controller {0}{1};",
                            Publish.codeGeneration.memberNamePrefix, con.Name, ""));
                        for (var j = 0; j < con.Pages.Count; j++)
                        {
                            var stateName = con.Pages[j];
                            if (string.IsNullOrEmpty(stateName))
                            {
                                stateName = string.Format("{0}", j);
                            }

                            variable.AppendLine(string.Format("{4,8}public const int {0}{1}_{2} = {3};", Publish.codeGeneration.memberNamePrefix, con.Name, stateName, j, ""));
                        }

                        variable.AppendLine("");

                        content.AppendLine(string.Format("{3,12}{0}{1} = this.GetControllerAt({2});", Publish.codeGeneration.memberNamePrefix, con.Name, i, ""));
                    }


                    var compIndex = 0;
                    for (var i = 0; i < comp.ComponentDescription.DisplayList.Count; i++)
                    {
                        var dispComp = comp.ComponentDescription.DisplayList[i];
                        var compType = "GComponent";
                        compType = dispComp.GetType().Name;
                        if (dispComp is CustomComponent)
                        {
                            var c = (CustomComponent)dispComp;
                            var pkgId = c.Pkg;
                            if (string.IsNullOrEmpty(pkgId))
                            {
                                pkgId = pkgKey;
                            }

                            compType = GetClassName(Components[pkgId][c.Src]);
                        }
                        else if (dispComp is Text)
                        {
                            compType = "GTextField";
                            var t = (Text)dispComp;
                            if (t.Input)
                            {
                                compType = "GTextInput";
                            }
                        }
                        else if (dispComp is RichText)
                        {
                            compType = "GRichTextField";
                        }
                        else if (dispComp is Group)
                        {
                            var g = (Group)dispComp;
                            if (g.Advanced == false)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            compType = string.Format("G{0}", compType);
                        }

                        if (IsDefaultName(dispComp.Name) == true)
                        {
                            if (Publish.codeGeneration.ignoreNoname)
                            {
                                compIndex++;
                                continue;
                            }
                        }
                        variable.AppendLine(string.Format("{3,8}public {0} {1}{2};", compType, Publish.codeGeneration.memberNamePrefix, dispComp.Name, " "));
                        content.AppendLine(string.Format("{4,12}{0}{1} = ({2})this.GetChildAt({3});",
                            Publish.codeGeneration.memberNamePrefix, dispComp.Name, compType, compIndex++, " "));
                    }

                    for (var i = 0; i < comp.ComponentDescription.Transitions.Count; i++)
                    {
                        var tran = comp.ComponentDescription.Transitions[i];
                        variable.AppendLine(string.Format("{2,8}public Transition {0}{1};", Publish.codeGeneration.memberNamePrefix, tran.Name, ""));
                        content.AppendLine(string.Format("{3,12}{0}{1} = this.GetTransitionAt({2});", Publish.codeGeneration.memberNamePrefix, tran.Name, i, ""));
                    }

                    str = str.Replace("{packageName}", packageName);
                    str = str.Replace("{className}", className);
                    str = str.Replace("{componentName}", componentName);
                    str = str.Replace("{uiPath}", uiPath);
                    str = str.Replace("{createInstance}", createInstance);
                    str = str.Replace("{variable}", variable.ToString());
                    str = str.Replace("{content}", content.ToString());

                    File.WriteAllText(filePath, str);
                }
            }
        }

        private string GetClassName(FairyComponent comp)
        {
            var className = comp.ResoureInfo.Name.Replace(".xml", "");
            className = className.Replace("&", "_");
            className = string.Format("{0}{1}", Publish.codeGeneration.classNamePrefix, className);
            return className;
        }

        private static bool IsDefaultName(string memberName)
        {
            if (memberName.Length < 2)
            {
                return false;
            }
            if (memberName[0] != 'n')
            {
                return false;
            }

            for (int i = 1; i < memberName.Length; i++)
            {
                if (memberName[i] > '9' || memberName[i] < '0')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
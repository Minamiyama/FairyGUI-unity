using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FairyAnalyzer.Component;
using UnityEditor;
using UnityEngine;

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

        public List<string> Output(string outputPath)
        {
            var processDict = BuildOutputList();

            var filePathes = new List<string>();

            //开始处理输出
            var classTemplate = (TextAsset)EditorGUIUtility.Load("FairyAnalyzer/Component.template.txt");
            var pkgKeys = processDict.Keys.ToList();
            var pkgStep = 1f / pkgKeys.Count;

            for (var k = 0; k < pkgKeys.Count; k++)
            {
                var pkgKey = pkgKeys[k];

                var package = Packages[pkgKey];
                EditorUtility.DisplayProgressBar("导出...", package.FolderName, k * pkgStep);

                var pkgOutputPath = string.Format("{0}/{1}", outputPath, package.FolderName);
                if (Directory.Exists(pkgOutputPath))
                {
                    Directory.Delete(pkgOutputPath, true);
                }
                Directory.CreateDirectory(pkgOutputPath);

                var bindContent = new StringBuilder();

                var compKeys = processDict[pkgKey].Keys.ToList();
                for (var cIndex = 0; cIndex < compKeys.Count; cIndex++)
                {
                    var compKey = compKeys[cIndex];
                    var comp = processDict[pkgKey][compKey];
                    var fairyComponentName = comp.ResoureInfo.Name.Replace(".xml", "");

                    EditorUtility.DisplayProgressBar("导出...",
                        string.Format("{0}/{1}", package.FolderName, fairyComponentName), (k * pkgStep) + (cIndex * 1f / compKeys.Count) * pkgStep);


                    string className = GetClassName(comp);

                    var filePath = string.Format("{0}/{1}.cs", pkgOutputPath, className);
                    var str = classTemplate.text;

                    var componentName = "GComponent";
                    if (string.IsNullOrEmpty(comp.ComponentDescription.Extention) == false)
                    {
                        componentName = string.Format("G{0}", comp.ComponentDescription.Extention);
                    }

                    var uiPath = string.Format("ui://{0}{1}", pkgKey, compKey);

                    var createInstance = string.Format("{3,12}return ({0})UIPackage.CreateObject(\"{1}\", \"{2}\");", className, GetPackageName(package), fairyComponentName, " ");

                    var variable = new StringBuilder();
                    var content = new StringBuilder();

                    var controllerPath = GenerateController(package, comp, filePath, variable, content);
                    if (string.IsNullOrEmpty(controllerPath) == false)
                    {
                        filePathes.Add(controllerPath);
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
                            if (string.IsNullOrEmpty(c.Pkg))
                            {
                                compType = GetClassName(Components[package.PackageID][c.Src]);
                            }
                            else //跨包引用加包名
                            {
                                compType = GetClassFullName(Components[c.Pkg][c.Src]);
                            }
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

                            compType = "GGroup";
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

                    str = str.Replace("{packageName}", GetPackageFullName(package));
                    str = str.Replace("{className}", className);
                    str = str.Replace("{componentName}", componentName);
                    str = str.Replace("{uiPath}", uiPath);
                    str = str.Replace("{createInstance}", createInstance);
                    str = str.Replace("{variable}", variable.ToString());
                    str = str.Replace("{content}", content.ToString());
                    bindContent.AppendLine(string.Format(
                        "{1,12}UIObjectFactory.SetPackageItemExtension({0}.URL, () => (GComponent)Activator.CreateInstance(typeof({0})));",
                        className, ""));
                    File.WriteAllText(filePath, str);
                    filePathes.Add(filePath);
                }

                var bindText = ((TextAsset)EditorGUIUtility.Load("FairyAnalyzer/Binder.template.txt")).text;
                bindText = bindText.Replace("{packageName}", GetPackageFullName(package));
                bindText = bindText.Replace("{className}", string.Format("{0}Binder", GetPackageName(package)));
                bindText = bindText.Replace("{bindContent}", bindContent.ToString());
                var outPath = string.Format("{0}/{1}Binder.cs", pkgOutputPath, GetPackageName(package));
                File.WriteAllText(outPath, bindText);
                filePathes.Add(outPath);
            }
            EditorUtility.ClearProgressBar();
            return filePathes;
        }

        private Dictionary<string, Dictionary<string, FairyComponent>> BuildOutputList()
        {
            var outputList = new List<FairyComponent>();
            var dealed = new Dictionary<string, Dictionary<string, FairyComponent>>();
            foreach (var pkgKey in Components.Keys)
            {
                Debug.Log(string.Format("[Package]********************************{0}", Packages[pkgKey].FolderName));
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

            //TODO 可以融合进下一步
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
                    if (componentType is CustomComponent)
                    {
                        var customComponent = (CustomComponent)componentType;
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
                    else if (componentType is List)
                    {
                        var listComp = (List)componentType;
                        if (string.IsNullOrEmpty(listComp.DefaultItem) == false)
                        {
                            outputList.Add(GetComponentFromUiUri(listComp.DefaultItem));
                        }

                        foreach (var listCompItem in listComp.items)
                        {
                            if (string.IsNullOrEmpty(listCompItem.Url) == false)
                            {
                                outputList.Add(GetComponentFromUiUri(listCompItem.Url));
                            }
                        }
                    }
                }
            }

            return dealed;
        }

        /// <summary>
        /// 生成Controller文件
        /// </summary>
        /// <param name="package"></param>
        /// <param name="comp"></param>
        /// <param name="filePath"></param>
        /// <param name="variable"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private string GenerateController(FairyPackage package, FairyComponent comp, string filePath, StringBuilder variable, StringBuilder content)
        {
            var controllerTemplate = ((TextAsset)EditorGUIUtility.Load("FairyAnalyzer/Controller.template.txt")).text;
            var controllerTemplateParts = controllerTemplate.Split(new[] { "-----" }, StringSplitOptions.None);

            var controllerTemplateHeader = controllerTemplateParts[0];

            controllerTemplateHeader = controllerTemplateHeader.Replace("{packageName}", GetPackageFullName(package));
            controllerTemplateHeader = controllerTemplateHeader.Replace("{className}", GetClassName(comp));

            var controllerTemplateFooter = controllerTemplateParts[2];
            var controllerTemplateBody = new StringBuilder();

            for (var i = 0; i < comp.ComponentDescription.Controllers.Count; i++)
            {
                var controllerDef = controllerTemplateParts[1];
                var con = comp.ComponentDescription.Controllers[i];

                var memberName = string.Format("{0}{1}", Publish.codeGeneration.memberNamePrefix, con.Name);

                var controllerIndexMember = new StringBuilder();
                for (var j = 0; j < con.Pages.Count; j++)
                {
                    var stateName = con.Pages[j];
                    stateName = string.Format("{0}_{1}", stateName, j);

                    controllerIndexMember.AppendLine(string.Format("{3,16}{0}{1} = {2},", memberName, stateName, j, ""));
                }


                controllerDef = controllerDef.Replace("{controllerName}", con.Name);
                controllerDef = controllerDef.Replace("{controllerIndexMember}", controllerIndexMember.ToString());
                controllerTemplateBody.AppendLine(controllerDef);

                variable.AppendLine(string.Format("{2,8}public {0}Controller {1};", con.Name, memberName, ""));
                content.AppendLine(string.Format("{3,12}{0} = ({1}Controller)this.GetControllerAt({2});", memberName, con.Name, i, ""));
            }

            var controllerBody = controllerTemplateBody.ToString();
            if (string.IsNullOrEmpty(controllerBody) == false)
            {
                controllerBody = string.Format("{0}{1}{2}", controllerTemplateHeader, controllerBody, controllerTemplateFooter);
                var outPath = filePath.Replace(".cs", "Controller.cs");
                File.WriteAllText(outPath, controllerBody);
                return outPath;
            }

            return string.Empty;
        }

        private string GetClassName(FairyComponent comp)
        {
            var className = comp.ResoureInfo.Name.Replace(".xml", "");
            className = className.Replace("&", "_");
            className = string.Format("{0}{1}", Publish.codeGeneration.classNamePrefix, className);
            return className;
        }

        private string GetClassFullName(FairyComponent comp)
        {
            var refPkgName = GetPackageFullName(Packages[comp.PackageID]);
            refPkgName = string.Format("{0}.", refPkgName);
            var compType = string.Format("{0}{1}", refPkgName, GetClassName(comp));
            return compType;
        }

        public string GetPackageName(FairyPackage package)
        {
            return package.FolderName;
        }

        public string GetPackageFullName(FairyPackage package)
        {
            var fullName = GetPackageName(package);
            if (string.IsNullOrEmpty(Publish.codeGeneration.packageName) == false)
            {
                fullName = string.Format("{0}.{1}", Publish.codeGeneration.packageName, fullName);
            }

            return fullName;
        }

        public FairyComponent GetComponentFromUiUri(string uri)
        {
            return Components[uri.Substring(5, 8)][uri.Substring(13, 6)];
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
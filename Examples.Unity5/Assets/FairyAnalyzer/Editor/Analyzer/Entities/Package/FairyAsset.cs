using System;
using System.Collections.Generic;
using System.IO;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// Fairy资产
    /// </summary>
    public class FairyAsset
    {
        public Dictionary<string, FairyPackage> Packages;
        public Setting.Publish Publish;

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
            foreach (var packagePath in packagePathes)
            {
                var package = new FairyPackage(packagePath);
                Packages[package.PackageID] = package;
            }
        }

        public void Output(string outputPath)
        {
            foreach (var packageKey in Packages.Keys)
            {
                var package = Packages[packageKey];
                if (package.PackageDescription.Publish.GenCode)
                {
                    var packagePath = Path.Combine(outputPath, package.PackageDescription.Publish.Name);
                    if (Directory.Exists(packagePath) == false)
                    {
                        Directory.CreateDirectory(packagePath);
                    }

                    // 1.分析哪些组建需要输出，构建依赖树第一层
                    // 2.遍历第一层需要输出的组件进一步构造依赖树
                    // 3.输出总的绑定文件
                }
            }
        }
    }
}
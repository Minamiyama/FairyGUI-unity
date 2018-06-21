using System.Collections.Generic;
using System.IO;
using FairyAnalyzer.Component;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// Fairy包
    /// </summary>
    public class FairyPackage
    {
        public string PackageID { get; set; }
        public PackageDescription PackageDescription { get; set; }
        public Dictionary<string, FairyComponent> Components { get; set; }

        public FairyPackage(string packagePath)
        {
            PackageDescription = PackageDescription.Parse(packagePath);
            PackageID = PackageDescription.ID;
            Components = new Dictionary<string, FairyComponent>();
            foreach (var resource in PackageDescription.Resources)
            {
                var component = resource as Component;
                if (component != null)
                {
                    var compPath = string.Format("{0}{1}{2}", packagePath, component.Path, component.Name);
                    Components[component.ID] = new FairyComponent(PackageID, component, compPath);
                }
            }
        }

        public void Output(string outputPath, FairyAsset asset)
        {
            var compDescs = new Dictionary<string, Dictionary<string, ComponentDescription>>();

            var pkg_comp_outputed = new Dictionary<string, Dictionary<string, bool>>();

            if (PackageDescription.Publish.GenCode)
            {
                var packagePath = Path.Combine(outputPath, PackageDescription.Publish.Name);
                if (Directory.Exists(packagePath) == false)
                {
                    Directory.CreateDirectory(packagePath);
                }

                // 1.分析哪些组建需要输出，构建依赖树第一层
                foreach (var componentKey in Components.Keys)
                {
                    var component = Components[componentKey];
                }

                // 2.遍历第一层需要输出的组件进一步构造依赖树
                // 3.输出总的绑定文件
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public string FolderName { get; set; }

        public FairyPackage(string packagePath)
        {
            PackageDescription = PackageDescription.Parse(packagePath);
            PackageID = PackageDescription.ID;
            Components = new Dictionary<string, FairyComponent>();
            var splits = packagePath.Split(new[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries);
            FolderName = splits.Last();

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
    }
}
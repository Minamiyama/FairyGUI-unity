using System.Collections.Generic;
using FairyAnalyzer.Component;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// Fairy包
    /// </summary>
    public class FairyPackage
    {
        public string PackageID { get; set; }
        public PackageDescription Package { get; set; }
        public Dictionary<string, ComponentDescription> Components { get; set; }

        public FairyPackage(string packagePath)
        {
            Package = PackageDescription.Parse(packagePath);
            PackageID = Package.ID;
            Components = new Dictionary<string, ComponentDescription>();
            foreach (var resource in Package.Resources)
            {
                if (resource is Component)
                {
                    var compDesc =
                        ComponentDescription.Parse(
                            string.Format("{0}{1}{2}", packagePath, resource.Path, resource.Name));
                    Components[resource.ID] = compDesc;
                }
            }
        }
    }
}
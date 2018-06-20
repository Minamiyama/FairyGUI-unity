using System.Collections.Generic;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// Fairy资产
    /// </summary>
    public class FairyAsset
    {
        public Dictionary<string, FairyPackage> Packages;

        public FairyAsset(string[] packagePathes)
        {
            Packages = new Dictionary<string, FairyPackage>();
            foreach (var packagePath in packagePathes)
            {
                var package = new FairyPackage(packagePath);
                Packages[package.PackageID] = package;
            }
        }
    }
}
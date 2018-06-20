namespace FairyAnalyzer.Package
{
    public class PackageLoader
    {
        public static void Load(string packagePath)
        {
            var asset = new FairyAsset(packagePath);
        }
    }
}
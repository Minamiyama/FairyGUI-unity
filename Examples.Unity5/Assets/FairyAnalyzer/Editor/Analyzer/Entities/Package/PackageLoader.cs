namespace FairyAnalyzer.Package
{
    public class PackageLoader
    {
        public static void Load(string packagePath, string outputPath)
        {
            var asset = new FairyAsset(packagePath);
            asset.Output(outputPath);
        }
    }
}
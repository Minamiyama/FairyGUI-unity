using FairyAnalyzer.Component;

namespace FairyAnalyzer.Package
{
    /// <summary>
    /// Fairy组件
    /// </summary>
    public class FairyComponent
    {
        public string PackageID { get; set; }
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public ComponentDescription ComponentDescription { get; set; }
        public Component ResoureInfo { get; set; }

        public FairyComponent(string packageId, Component res, string componentPath)
        {
            PackageID = packageId;
            ResoureInfo = res;
            ComponentID = res.ID;
            ComponentDescription = ComponentDescription.Parse(componentPath);
        }
    }
}
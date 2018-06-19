using System.Collections.Generic;

namespace FairyAnalyzer.Package
{
    public abstract class ResourceBase : FairyItemBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Exported { get; set; }
    }


    public class Image : ResourceBase { }
    public class Component : ResourceBase { }
    public class MovieClip : ResourceBase { }
}

namespace FairyAnalyzer
{
    public class ComponentDescription
    {
        public Controller Controller { get; set; }
        public List<Component> DisplayList { get; set; }
    }

    public abstract class NameBase
    {
        public string Name { get; set; }
    }

    public class Controller : NameBase
    {
    }

    public abstract class IdBase : NameBase
    {
        public string ID { get; set; }
    }

    public class Component : IdBase
    {
    }

    public class Group : Component { }

    public class Text : Component { }
}
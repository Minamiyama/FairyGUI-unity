using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyAnalyzer.Package
{
    public class PackageDescription : FairyItemBase
    {
        public List<FairyItemBase> Resources { get; set; }
        public Publish Publish { get; set; }
    }


}

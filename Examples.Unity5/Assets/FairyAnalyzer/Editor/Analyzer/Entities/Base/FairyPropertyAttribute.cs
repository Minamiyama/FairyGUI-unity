using System;

namespace FairyAnalyzer.Base
{
    public class FairyPropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public FairyPropertyAttribute()
        {

        }

        public FairyPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}
using System;

namespace FairyAnalyzer.Component
{
    public class ExtentionType
    {

    }
    
    [Serializable]
    public class Button : ExtentionType { }

    [Serializable]
    public class Label : ExtentionType { }

    [Serializable]
    public class ProgressBar : ExtentionType { }

    [Serializable]
    public class ComboBox : ExtentionType { }

    [Serializable]
    public class Slider : ExtentionType { }

    [Serializable]
    public class ScrollBar : ExtentionType { }
}

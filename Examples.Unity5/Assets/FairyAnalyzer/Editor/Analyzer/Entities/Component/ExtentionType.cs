using System;

namespace FairyAnalyzer.Component
{
    /// <summary>
    /// 扩展类型
    /// </summary>
    [Serializable]
    public class ExtentionType
    {

    }
    
    /// <summary>
    /// 按钮
    /// </summary>
    [Serializable]
    public class Button : ExtentionType { }

    /// <summary>
    /// 标签
    /// </summary>
    [Serializable]
    public class Label : ExtentionType { }

    /// <summary>
    /// 进度条
    /// </summary>
    [Serializable]
    public class ProgressBar : ExtentionType { }

    /// <summary>
    /// 下拉框
    /// </summary>
    [Serializable]
    public class ComboBox : ExtentionType { }

    /// <summary>
    /// 滑动条
    /// </summary>
    [Serializable]
    public class Slider : ExtentionType { }

    /// <summary>
    /// 滚动条
    /// </summary>
    [Serializable]
    public class ScrollBar : ExtentionType { }
}

// // ================================================================
// // FileName:FairyGenerateCodeModel.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: 生成数据模型
// // ================================================================

using System.Collections.Generic;
using UnityEngine;

namespace FairyAnalyzer
{
    [System.Serializable]
    public class FairyGenerateCodeModel : ScriptableObject
    {
        /// <summary>
        /// UI 工程的根目录
        /// </summary>
        public string UIProjectRootPath = "";

        /// <summary>
        /// 包队列
        /// </summary>
        public List<FairyGUIPackageInfo> PackageInfos = new List<FairyGUIPackageInfo>();
    }


    /// <summary>
    /// fairy gui的包相关editor定义
    /// </summary>
    [System.Serializable]
    public class FairyGUIPackageInfo
    {
        /// <summary>
        /// 相对路径
        /// </summary>
        public string PackageRelativePath;

        /// <summary>
        /// 包名称
        /// </summary>
        public string PackageName;

        /// <summary>
        /// 是否导出此包代码
        /// </summary>
        public bool IsExport;

        /// <summary>
        /// 组件队列
        /// </summary>
        public List<FairyGUIComponentInfo> PackageInfos = new List<FairyGUIComponentInfo>();
    }

    /// <summary>
    /// 组件信息
    /// </summary>
    [System.Serializable]
    public class FairyGUIComponentInfo
    {
        /// <summary>
        /// 组件的相对路径
        /// </summary>
        public string PackageName;

        /// <summary>
        /// 包名称
        /// </summary>
        public string ComponentName;

        /// <summary>
        /// fairy中是否导出
        /// </summary>
        public bool isFairyIsExport;

        /// <summary>
        /// 是否生成代码
        /// </summary>
        public bool IsGenerateCode;
    }
}
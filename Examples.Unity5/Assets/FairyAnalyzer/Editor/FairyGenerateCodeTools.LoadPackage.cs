// // ================================================================
// // FileName:FairyGenerateCodeTools.LoadPackage.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: 主要处理加载包结构部分逻辑
// // ================================================================

using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace FairyAnalyzer
{
    public partial class FairyGenerateCodeTools
    {
        /// <summary>
        /// 加载所有子文件夹
        /// </summary>
        private void LoadAllPackageDir()
        {
            List<DirectoryInfo> allDirInfos = new List<DirectoryInfo>();
            if (false == string.IsNullOrEmpty(model.UIProjectRootPath))
            {                
                FileHelper.GetAllDirBySub(new DirectoryInfo(model.UIProjectRootPath + "/assets"), allDirInfos);
                model.PackageInfos.Clear();
                if (allDirInfos.Count > 0)
                {
                    // 解析出所有的包
                    for (var index = 0; index < allDirInfos.Count; index++)
                    {
                        EditorUtility.DisplayProgressBar("Refresh", "Refresh Fariy GUI Packages", index * 1.0f / allDirInfos.Count);
                        var directoryInfo = allDirInfos[index];
                        if (null == directoryInfo)
                        {
                            continue;
                        }

                        var packageInfo = new FairyGUIPackageInfo()
                        {
                            PackageRelativePath = string.Format("assets/{0}/package.xml", directoryInfo),
                            PackageName         = directoryInfo.Name
                        };
                        model.PackageInfos.Add(packageInfo);

                        // 解析出包里面的所有组件
                        var componentsList = XMLParseUtil.GetAllComponentByPackagePath(directoryInfo.Name, string.Format("{0}/package.xml", directoryInfo.FullName));
                        packageInfo.PackageInfos.Clear();
                        foreach (var componentItem in componentsList)
                        {
                            packageInfo.PackageInfos.Add(componentItem);
                            UnityEngine.Debug.Log("添加了包 = " + componentItem.ComponentName);
                        }
                    }

                    EditorUtility.ClearProgressBar();
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using FairyAnalyzer.Package;
using UnityEngine;
using UnityEditor;

public class Menu
{
    [MenuItem("工具/解析")]
    public static void Parse()
    {
        var packagePath = string.Format("{0}/../UIProject/UIProject/assets/Basics", Application.dataPath);
        var package = PackageDescription.Parse(packagePath);
    }
}

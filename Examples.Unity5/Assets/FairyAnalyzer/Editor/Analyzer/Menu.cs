using System.Collections;
using System.Collections.Generic;
using FairyAnalyzer.Package;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Menu
{
    [MenuItem("工具/打开GUI项目")]
    public static void Process()
    {
        var argPath = Path.Combine("UIProject/UIProject", "FairyGUI-Examples.fairy");
        var guiBat = Path.Combine("UIProject/UIProject", "FairyGUI-Editor");
        var exePath = Path.Combine(guiBat, "FairyGUI-Editor.exe");

        argPath = Path.Combine(Directory.GetCurrentDirectory(), argPath);
        exePath = Path.Combine(Directory.GetCurrentDirectory(), exePath);

        System.Diagnostics.Process.Start(exePath, argPath);
    }

    [MenuItem("工具/解析")]
    public static void Parse()
    {
        var packagePath = string.Format("{0}/../UIProject/UIProject", Application.dataPath);
        var outputPath = string.Format("{0}/../Output", packagePath);
        PackageLoader.Load(packagePath, outputPath);
    }
}

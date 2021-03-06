﻿// // ================================================================
// // FileName:FairyGenerateCodeTools.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: Fairy GUI 生成代码的主要工具类
// // ================================================================

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FairyAnalyzer
{
    [CustomEditor(typeof(FairyGenerateCodeModel))]
    public partial class FairyGenerateCodeTools : BaseEditor
    {
        /// <summary>
        /// 菜单
        /// </summary>
        private readonly string[] menuItemNames = new string[] {"Rrfresh", "Generate Code"};

        /// <summary>
        /// model
        /// </summary>
        private FairyGenerateCodeModel model;

        /// <summary>
        /// 搜索的关键字
        /// </summary>
        private string searchKeyword = "";

        /// <summary>
        /// 搜索结果队列
        /// </summary>
        private List<FairyGUIComponentInfo> searchResult = new List<FairyGUIComponentInfo>();

        /// <summary>
        /// 重绘 Inspector
        /// </summary>
        public override void OnInspectorGUI()
        {
            model = (FairyGenerateCodeModel) target;
            if (null == model)
            {
                return;
            }


            AddToolBar(menuItemNames, RefreshPackageDir, GenerateCode); // 添加菜单选项
            var newSearchKey = SearchField(searchKeyword);              // 搜索条
            if (true == string.IsNullOrEmpty(model.UIProjectRootPath))
            {
                EditorGUILayout.HelpBox("请先选择UI工程根目录后，再执行导出\n这里填写FairyGUI工程的绝对路径 \n ps: D:\\XXX\\XXX\\FairyGUI-unity\\Examples.Unity5\\UIProject\\UIProject", MessageType.Error);
            }

            // 绘制根目录文件夹
            using (new VerViewBlock("box"))
            {
                model.UIProjectRootPath = EditorGUILayout.TextField("  UI Project Root", model.UIProjectRootPath);
                using (new ColorBlock(Color.cyan))
                {
                    EditorGUILayout.LabelField("  Package Count = " + model.PackageInfos.Count);
                }
            }

            // 绘制搜索结果
            if (newSearchKey != searchKeyword)
            {
                FilterSearchResult(newSearchKey);
            }

            if (searchResult.Count > 0)
            {
                for (int index = 0; index < searchResult.Count; index++)
                {
                    var item = searchResult[index];
                    if (null != item)
                    {
                        EditorGUILayout.Space();
                        using (new ColorBlock(true == item.IsGenerateCode ? Color.green : Color.white))
                        {
                            using (new HorViewBlock())
                            {
                                item.IsGenerateCode =
                                    EditorGUILayout.Toggle(string.Format("[{2}] \t {0}\t[{1}]", item.ComponentName, item.isFairyIsExport, item.PackageName), item.IsGenerateCode);
                                if (GUILayout.Button("Generate code", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
                                {
                                    
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (true == string.IsNullOrEmpty(newSearchKey))
                {
                    // 绘制包队列
                    for (int index = 0; index < model.PackageInfos.Count; index++)
                    {
                        var package = model.PackageInfos[index];
                        if (null == package)
                        {
                            continue;
                        }

                        ContentBlock.DrawContent(package.PackageName, () =>
                        {
                            using (new VerViewBlock("box"))
                            {
                                foreach (var componentInfo in package.PackageInfos)
                                {
                                    using (new ColorBlock(true == componentInfo.IsGenerateCode ? Color.green : Color.white))
                                    {
                                        EditorGUILayout.Space();
                                        using (new HorViewBlock())
                                        {
                                            componentInfo.IsGenerateCode =
                                                EditorGUILayout.Toggle(string.Format("{0}\t[{1}]", componentInfo.ComponentName, componentInfo.isFairyIsExport), componentInfo.IsGenerateCode);
                                            if (GUILayout.Button("Generate code", EditorStyles.toolbarButton, GUILayout.ExpandWidth(false)))
                                            {
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 刷新文件夹
        /// </summary>
        private void RefreshPackageDir()
        {
            LoadAllPackageDir();
            if (null != model)
            {
                EditorUtility.SetDirty(model);
            }

            // 保存一下场景
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// 搜索结果
        /// </summary>
        private void FilterSearchResult(string _key)
        {
            searchKeyword = _key;
            searchResult.Clear();
            for (int index = 0; index < model.PackageInfos.Count; index++)
            {
                var package = model.PackageInfos[index];
                foreach (var componentInfo in package.PackageInfos)
                {
                    if (true == componentInfo.ComponentName.Contains(_key))
                    {
                        searchResult.Add(componentInfo);
                    }
                }
            }
        }


        /// <summary>
        /// 生成代码部分
        /// </summary>
        private void GenerateCode()
        {
        }

//        [MenuItem("Tools/Generate Tools Asset")]
//        public static void GenerateTools()
//        {
//            FairyGenerateCodeModel model = ScriptableObject.CreateInstance<FairyGenerateCodeModel>();
//            AssetDatabase.CreateAsset(model, "Assets/FairyAnalyzer.asset");
//            AssetDatabase.Refresh();
//        }
    }
}
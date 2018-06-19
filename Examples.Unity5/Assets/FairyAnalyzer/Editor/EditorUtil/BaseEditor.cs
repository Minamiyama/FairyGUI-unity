// // ================================================================
// // FileName:BaseEditor.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: Editor基类，用于扩展editor公用方法
// // ================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace FairyAnalyzer
{
    public class BaseEditor : Editor
    {
        /// <summary>
        /// 添加一个功能条
        /// </summary>
        /// <param name="_buttonNames"></param>
        /// <param name="_action"></param>
        public static void AddToolBar(string[] _buttonNames, params Action[] _action)
        {
            if (_buttonNames.Length == 0)
            {
                return;
            }

            using (
                new HorViewBlock(EditorStyles.toolbar,
                    new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(22) }))
            {
                for (int index = 0; index < _buttonNames.Length; index++)
                {
                    var buttonName = _buttonNames[index];
                    if (true == string.IsNullOrEmpty(buttonName))
                    {
                        continue;
                    }

                    Rect createBtnRect = GUILayoutUtility.GetRect(new GUIContent(buttonName), EditorStyles.toolbarButton,
                        GUILayout.ExpandWidth(false));
                    if (GUI.Button(createBtnRect, buttonName, EditorStyles.toolbarButton))
                    {
                        if (_action.Length >= _buttonNames.Length)
                        {
                            Action buttonAction = _action[index];
                            if (null != buttonAction)
                            {
                                buttonAction();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <returns></returns>
        public string OpenSelectDirDialog()
        {
            
//            OpenFileDialog.OpenFileName openFileName = new OpenFileDialog.OpenFileName();
//            openFileName.structSize   = Marshal.SizeOf(openFileName);
//            openFileName.filter       = "Excel文件(*.xlsx)\0*.xlsx";
//            openFileName.file         = new string(new char[256]);
//            openFileName.maxFile      = openFileName.file.Length;
//            openFileName.fileTitle    = new string(new char[64]);
//            openFileName.maxFileTitle = openFileName.fileTitle.Length;
//            openFileName.initialDir   = Application.streamingAssetsPath.Replace('/', '\\'); //默认路径
//            openFileName.title        = "窗口标题";
//            openFileName.flags        = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
//
//            if (OpenFileDialog.LocalDialog.GetOpenFileName(openFileName))
//            {
//                Debug.Log(openFileName.file);
//                Debug.Log(openFileName.fileTitle);
//            }
//
//            return "";
            return "";
        }
    }
}
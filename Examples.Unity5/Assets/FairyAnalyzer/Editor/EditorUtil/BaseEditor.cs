// // ================================================================
// // FileName:BaseEditor.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: Editor基类，用于扩展editor公用方法
// // ================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
        /// 搜索条
        /// </summary>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string SearchField(string value, params GUILayoutOption[] options)
        {
            MethodInfo info = typeof(EditorGUILayout).GetMethod("ToolbarSearchField", BindingFlags.NonPublic | BindingFlags.Static, null, new System.Type[] { typeof(string), typeof(GUILayoutOption[]) }, null);
            if (info != null)
            {
                value = (string)info.Invoke(null, new object[] { value, options });
            }
            return value;
        }
    }
}
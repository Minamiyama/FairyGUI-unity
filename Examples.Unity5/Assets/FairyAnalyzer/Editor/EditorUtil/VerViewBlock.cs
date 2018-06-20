// // ================================================================
// // FileName:VerViewBlock.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: 纵向布局的绘制
// // ================================================================
using System;
using UnityEditor;
using UnityEngine;

namespace FairyAnalyzer
{
    public class VerViewBlock : IDisposable
    {
        public VerViewBlock(GUIStyle _style = null, GUILayoutOption [] _option = null)
        {
            if (null == _style)
            {
                _style = GUIStyle.none;
            }

            EditorGUILayout.BeginVertical(_style, _option);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
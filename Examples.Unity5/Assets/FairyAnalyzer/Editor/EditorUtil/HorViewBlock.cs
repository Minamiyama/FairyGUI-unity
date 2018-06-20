// // ================================================================
// // FileName:HorViewBlock.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: 横向布局
// // ================================================================

using System;
using UnityEditor;
using UnityEngine;

public class HorViewBlock : IDisposable
{
    public HorViewBlock(GUIStyle _style = null, GUILayoutOption [] _option = null)
    {
        if (null == _style)
        {
            _style = GUIStyle.none;
        }

        EditorGUILayout.BeginHorizontal(_style, _option);
    }

    public void Dispose()
    {
        EditorGUILayout.EndHorizontal();
    }
}
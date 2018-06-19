// // ================================================================
// // FileName:ColorBlock.cs
// // User: Baron
// // CreateTime:2018/6/19
// // Description: 颜色设置
// // ================================================================
using System;
using UnityEngine;

namespace FairyAnalyzer
{
    public class ColorBlock : IDisposable
    {
        public ColorBlock(Color _color)
        {
            GUI.color = _color;
        }

        public void Dispose()
        {
            GUI.color = Color.white;
        }
    }
}
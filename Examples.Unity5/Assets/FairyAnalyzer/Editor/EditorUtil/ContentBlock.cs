using System;
using UnityEditor;
using UnityEngine;

namespace FairyAnalyzer
{
    public class ContentBlock : IDisposable
    {
        public ContentBlock(bool _minimalistic)
        {
            BeginContents(_minimalistic);
        }

        public void Dispose()
        {
            EndContents();
        }

        /// <summary>
        /// Begin drawing the content area.
        /// </summary>
        static bool mEndHorizontal = false;

        static private void BeginContents(bool minimalistic)
        {
            if (!minimalistic)
            {
                mEndHorizontal = true;
                GUILayout.BeginHorizontal();
                EditorGUILayout.BeginHorizontal(GUIStyle.none, GUILayout.MinHeight(10f));
            }
            else
            {
                mEndHorizontal = false;
                EditorGUILayout.BeginHorizontal(GUILayout.MinHeight(10f));
                GUILayout.Space(10f);
            }

            GUILayout.BeginVertical();
            GUILayout.Space(2f);
        }

        /// <summary>
        /// End drawing the content area.
        /// </summary>
        static private void EndContents()
        {
            GUILayout.Space(3f);
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            if (mEndHorizontal)
            {
                GUILayout.Space(3f);
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(3f);
        }

        /// <summary>
        /// 绘制一个分类
        /// </summary>
        /// <param name="_header">分类的头名字</param>
        /// <param name="_action">分类绘的具体内容回调接口</param>
        /// <param name="mini"></param>
        /// <param name="_space"></param>
        public static void DrawContent(string _header, Action _action, bool mini = false, float _space = 0)
        {
            if (true == string.IsNullOrEmpty(_header))
            {
                _header = " 未命名 ";
            }

            using (new HorViewBlock())
            {
                GUILayout.Space(5);
                using (new VerViewBlock())
                {
                    if (true == DrawHeader(_header))
                    {
                        using (new ContentBlock(mini))
                        {
                            if (null != _action)
                            {
                                _action();
                            }
                        }
                    }
                }
            }
        }

        static public bool DrawHeader(string text)
        {
            return DrawHeader(text, text, false, false);
        }

        static private bool DrawHeader(string text, string key, bool forceOn, bool minimalistic)
        {
            bool state = EditorPrefs.GetBool(key, false);
            if (!minimalistic) GUILayout.Space(3f);
            if (!forceOn && !state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
            GUILayout.BeginHorizontal();
            GUI.changed = false;
            if (minimalistic)
            {
                if (state) text = "\u25BC" + (char) 0x200a + text;
                else text       = "\u25BA" + (char) 0x200a + text;
                GUILayout.BeginHorizontal();
                GUI.contentColor = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.7f) : new Color(0f, 0f, 0f, 0.7f);
                if (!GUILayout.Toggle(true, text, "PreToolbar2", GUILayout.MinWidth(20f))) state = !state;
                GUI.contentColor = Color.white;
                GUILayout.EndHorizontal();
            }
            else
            {
                text = "<b><size=11>" + text + "</size></b>";
                if (state) text = "\u25BC " + text;
                else text       = "\u25BA " + text;
                if (!GUILayout.Toggle(true, text, "dragtab", GUILayout.MinWidth(20f))) state = !state;
            }

            if (GUI.changed) EditorPrefs.SetBool(key, state);
            if (!minimalistic) GUILayout.Space(2f);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            if (!forceOn && !state) GUILayout.Space(3f);
            return state;
        }
    }
}
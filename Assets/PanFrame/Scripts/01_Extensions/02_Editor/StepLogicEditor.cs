/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     StepLogicEditor.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-12
 *Description:   
 *History:
*/ 
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace SQFramework
{
#if UNITY_EDITOR
    public class StepLogicEditor : EditorWindow
    {
        [MenuItem("SQFramework/AutoBuilderTool", false, -39)]
        private static void CreateScripts()
        {
            Rect area = new Rect(0, 0, 300, 120);
            StepLogicEditor window = GetWindowWithRect<StepLogicEditor>(area, true, "步骤脚本生成器");
            window.Show();
        }

        static string savePath = string.Empty;
        static string saveFileName = string.Empty;
        // 0 Name 1 ControlUI 
        static string commonContent =
    "using UnityEngine;\n" +
    "public class {0}{1}:MonoBehaviour\r\n" +
    "{2}\r\n" +
    "    private MainControl mainC;\n" +
    "    private {0}{1}UI {4}UIC;\n" +
    "    public void Init(MainControl c)\n" +
    "    {2}\r\n" +
    "        mainC = c;\n" +
    "        {4}UIC = GetComponent<{0}{1}UI>();\n" +
    "        {4}UIC.Init(this);\n" +
    "    {3}\r\n" +
    "    public void OnEnter()\n" +
    "    {2}\r\n" +
    "        {4}UIC.OnEnter();\n" +
    "    {3}\r\n" +
    "    public void OnExit()\n" +
    "    {2}\r\n" +
    "        {4}UIC.OnExit();\n" +
    "    {3}\r\n" +
    "{3}";

        static string commonContentUI =
    "using UnityEngine;\n" +
    "using UnityEngine.UI;\n" +
    "public class {0}{1}UI:MonoBehaviour\r\n" +
    "{2}\r\n" +
    "    private {0}{1} {4}C;\n" +
    "    public void Init({0}{1} c)\r\n" +
    "    {2}\r\n\r\n" +
    "        {4}C = c;\n" +
    "    {3}\r\n\r\n" +
    "    public void OnEnter()\n" +
    "    {2}\r\n\r\n" +
    "    {3}\r\n" +
    "    public void OnExit()\n" +
    "    {2}\r\n\r\n" +
    "    {3}\r\n" +
    "{3}";

        private void OnGUI()
        {
            GUIStyle buttonStyle = new GUIStyle();
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.fontSize = 15;
            buttonStyle.fixedWidth = 15;
            buttonStyle.normal.textColor = Color.red;
            buttonStyle.normal.background = null;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("脚本名:", GUILayout.Width(80));
            saveFileName = EditorGUILayout.TextField(string.Empty, saveFileName, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("脚本生成路径:", GUILayout.Width(80));
            savePath = EditorGUILayout.TextField(string.Empty, savePath, GUILayout.Width(190));
            if (GUILayout.Button("⊙", buttonStyle))
            {
                savePath = EditorUtil.OpenFolder();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("生成"))
            {
                GUI.color = Color.blue;
                if (!Directory.Exists(savePath))
                {
                    Debug.LogError("路径不存在!!!");
                    return;
                }

                File.WriteAllText(savePath + saveFileName + "Control.cs",
                 string.Format(commonContent, saveFileName, "Control", '{', '}', saveFileName.ToLower()), Encoding.UTF8);

                File.WriteAllText(savePath + saveFileName + "ControlUI.cs",
                string.Format(commonContentUI, saveFileName, "Control", '{', '}', saveFileName.ToLower()), Encoding.UTF8);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("/////////文件创建成功//////" + saveFileName);
            }
            EditorGUILayout.EndHorizontal();
        }



    }

#endif
}
/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     PanWindow.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-12
 *Description:   
 *History: 
 */
using UnityEngine;

using UnityEditor;
 
namespace SQFramework {
#if UNITY_EDITOR
    public class PanWindowExample : EditorWindow
    {
        // %代表Ctrl &代表Alt
        [MenuItem("SQFramework/PanExample ", false, -40)]
        private static void Open()
        {
            //到这里我们的窗口就实现了，接下来要填充窗口里的东西了
            Rect area = new Rect(0, 0, 600, 400);
            PanWindowExample window = GetWindowWithRect<PanWindowExample>(area, true, "Panpan窗口"); //限定大小 
                                                                                     //PanWindow window = (PanWindow)EditorWindow.GetWindow(typeof(PanWindow)); //类似Inspector这种的
            window.Show();
        }
        public enum QEnum
        {
            None,
            One,
            Two
        }
        string myString = string.Empty;
        bool isBool = false;
        float myFloat = 0.0f;
        Color color;
        QEnum qEnum;
        GameObject go;
        private void OnGUI()
        {
            GUIStyle buttonStyle = new GUIStyle(); //声明一个样式
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.fontSize = 15;
            buttonStyle.fixedWidth = 15;
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.normal.background = null;
            //默认是一行一行的往下走    
            EditorGUILayout.Space(); //空格
                                     //标签
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();

            //GUILayout.Width(80) 定义自己的长度
            EditorGUILayout.LabelField("账号:", GUILayout.Width(80));
            //myString = EditorGUILayout.TextField("账号:", myString, GUILayout.Width(100));//这样排版不太好

            myString = EditorGUILayout.TextField(string.Empty, myString, GUILayout.Width(100));
            if (GUILayout.Button("生成", GUILayout.Width(40)))
            {

            }
            if (GUILayout.Button("⊙", buttonStyle))
            {

            }
            EditorGUILayout.EndHorizontal(); //下一行

            //控制子节点勾选
            isBool = EditorGUILayout.BeginToggleGroup("Optional Setting", isBool);
            isBool = EditorGUILayout.Toggle("Toggle", isBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();

            //颜色
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("给你点颜色看看:", GUILayout.Width(160));
            color = EditorGUILayout.ColorField(color, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal(); //下一行

            //文本框
            EditorGUILayout.BeginVertical("box", GUILayout.Width(160), GUILayout.Height(100));
            myString = EditorGUILayout.TextArea("sss", myString, GUILayout.Width(160), GUILayout.Height(100));
            EditorGUILayout.EndVertical();
            //枚举
            qEnum = (QEnum)EditorGUILayout.EnumPopup("qEnum", qEnum, GUILayout.Width(220));
            //下拉框

            go = EditorGUILayout.ObjectField(new GUIContent("物体:", "说明？？"), go, typeof(GameObject), true) as GameObject;

        }
         
    }

    public class PanWindow : EditorWindow
    {
        // %代表Ctrl &代表Alt
        [MenuItem("SQFramework/Pan %e", false, -40)]
        private static void Open()
        {
            //到这里我们的窗口就实现了，接下来要填充窗口里的东西了
            Rect area = new Rect(0, 0, 600, 400);
            PanWindow window = GetWindowWithRect<PanWindow>(area, true, "Panpan窗口"); //限定大小 
            window.Show();
        }  
        
        private void OnGUI()
        {
        
            //默认是一行一行的往下走    
            EditorGUILayout.Space(); //空格
                                     //标签
            GUILayout.Label("Code Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
              
            EditorGUILayout.LabelField("ConfigTools:pc端配置文件", GUILayout.Width(160));
            if (GUILayout.Button("下载", GUILayout.Width(40)))
            {

            } 
            EditorGUILayout.EndHorizontal();   
            //颜色
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("XMlTool:", GUILayout.Width(160));
            if (GUILayout.Button("下载", GUILayout.Width(40)))
            {

            }
            EditorGUILayout.EndHorizontal();  

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("JsonTool:", GUILayout.Width(160));
            if (GUILayout.Button("下载", GUILayout.Width(40)))
            {

            }
            EditorGUILayout.EndHorizontal(); //下一行 

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("SocketTool:", GUILayout.Width(160));
            if (GUILayout.Button("下载", GUILayout.Width(40)))
            {

            }
            EditorGUILayout.EndHorizontal(); //下一行 

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("FishTool:", GUILayout.Width(160));
            if (GUILayout.Button("下载", GUILayout.Width(40)))
            {
                DownLoadTools.DownTool(DownLoadTools.fishMovePath); 
            }
            EditorGUILayout.EndHorizontal(); //下一行 

        }

    }
#endif
}
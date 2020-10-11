/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     Exporter.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/
using System; 
using UnityEditor;
using UnityEngine;
namespace SQFramework
{
#if UNITY_EDITOR
    /// <summary>
    /// 导出类
    /// </summary>
    public class Exporter
    {

        [MenuItem("SQFramework/导出UnityPackage %t", false, 1)]
        private static void MenuClicked()
        {
            var generatePackageName = Exporter.GenerateUnityPackageName();

            EditorUtil.ExportPackage("Assets/PanFrame", generatePackageName + ".unitypackage");

            if (DirFileHelper.IsExistsFile(generatePackageName + ".unitypackage"))
            {
                Debug.Log("///嘎嘎，导出成功");
                //EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));
                EditorUtil.OpenInFolder(@""); 
            }
            else
            {
                Debug.LogError("///嘎嘎，导出失败");
            }

        }


        /// <summary>
        /// 根据时间命名
        /// </summary>
        /// <returns></returns>
        public static string GenerateUnityPackageName()
        {
            return "SQFramework_" + DateTime.Now.ToString("yyyyMMdd_hh");
        }

    }
#endif
}
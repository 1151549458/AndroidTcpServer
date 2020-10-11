/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     TransformExtion.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

namespace SQFramework
{
#if UNITY_EDITOR
    /// <summary>
    /// 编辑器类
    /// </summary>
    public class EditorUtil
    {
        // %是Ctrl &是Alt

        public static void OpenInFolder(string folderPath)
        {
            Application.OpenURL("file:///" + folderPath);
        }
        public static void OpenInFolder()
        {
            Application.OpenURL(@"");
        }
        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="assetPathName"></param>
        /// <param name="fileName"></param>
        public static void ExportPackage(string assetPathName, string fileName)
        {
            AssetDatabase.ExportPackage(assetPathName, fileName, ExportPackageOptions.Recurse);
        }

        public static void Refresh()
        {
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 执行哪个命令
        /// </summary>
        /// <param name="menuPath"></param>
        public static void CallMenuItem(string menuPath)
        {
            EditorApplication.ExecuteMenuItem(menuPath);
        }
        public static string OpenFolder()
        {
            string tmpPath = string.Empty;
            tmpPath = EditorUtility.OpenFolderPanel("Resource Folder", "Assets", string.Empty);
            if (!string.IsNullOrEmpty(tmpPath))
            {
                var gamePath = System.IO.Path.GetFullPath(".");//TODO - FileUtil.GetProjectRelativePath??
                gamePath = gamePath.Replace("\\", "/");
                if (tmpPath.Length > gamePath.Length && tmpPath.StartsWith(gamePath))
                    tmpPath = tmpPath.Remove(0, gamePath.Length + 1);
            }
            return tmpPath + '/';
        }
    }
#endif

}
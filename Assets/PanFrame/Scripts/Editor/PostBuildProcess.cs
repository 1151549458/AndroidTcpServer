using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;
using System.IO;
namespace SQFramework
{
#if UNITY_EDITOR
    public class PostBuildProcess
    {
        [PostProcessBuild(0)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {

#if !UNITY_ANDROID || !UNITY_IPONE 
            //需要拷贝出去的文件夹名字列表
            string[] copyDirs = { "PanFrame/Config" };
            foreach (string copyDir in copyDirs)
            {
                string curPath = Path.Combine(Application.dataPath, copyDir);
                string destPath = Path.Combine(Path.GetDirectoryName(pathToBuiltProject), copyDir);
                Directory.CreateDirectory(destPath);
                foreach (string filePath in Directory.GetFiles(curPath, "*.ini"))
                {
                    File.Copy(filePath, Path.Combine(destPath, Path.GetFileName(filePath)));
                }
            }

#endif
        }
    }
#endif

}
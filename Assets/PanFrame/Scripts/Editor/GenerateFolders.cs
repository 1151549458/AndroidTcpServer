using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SQFramework
{
    public class GenerateFolders : MonoBehaviour
    { 
#if UNITY_EDITOR
        [MenuItem("SQFramework/CreatFolders/一键创建文件夹")]
        private static void CreateBasicFolder()
        {
            GenerateFolder();
            Debug.Log("Folders Created");
        }

        [MenuItem("SQFramework/CreatFolders//特定文件夹")]
        private static void CreateAllFolder()
        {
            GenerateFolder(1);
            Debug.Log("Folders Created");
        }
        [MenuItem("SQFramework/CreatFolders/GameFramework文件夹")]
        private static void CreateGameFw()
        {
            string prjPath = Application.dataPath + "/";

            Directory.CreateDirectory(prjPath + "GameMain/Configs");
            Directory.CreateDirectory(prjPath + "GameMain/DataTables");
            Directory.CreateDirectory(prjPath + "GameMain/Prefabs/Model");
            Directory.CreateDirectory(prjPath + "GameMain/Prefabs/UI");
            Directory.CreateDirectory(prjPath + "GameMain/Fonts");
            Directory.CreateDirectory(prjPath + "GameMain/Libraries");
            Directory.CreateDirectory(prjPath + "GameMain/Localization");
            Directory.CreateDirectory(prjPath + "GameMain/Materials");
            Directory.CreateDirectory(prjPath + "GameMain/Meshes");
            Directory.CreateDirectory(prjPath + "GameMain/Music");
            Directory.CreateDirectory(prjPath + "GameMain/Scenes");
            Directory.CreateDirectory(prjPath + "GameMain/Scripts");
            Directory.CreateDirectory(prjPath + "GameMain/Sounds");
            Directory.CreateDirectory(prjPath + "GameMain/Textures");
            Directory.CreateDirectory(prjPath + "GameMain/UI");
            AssetDatabase.Refresh();

            Debug.Log("Folders Created");
        }

        private static void GenerateFolder(int flag = 0)
        {
            // 文件路径
            string prjPath = Application.dataPath + "/";

            Directory.CreateDirectory(prjPath + "Achive/UI");
            Directory.CreateDirectory(prjPath + "Achive/Textures");
            Directory.CreateDirectory(prjPath + "Achive/Model");
            Directory.CreateDirectory(prjPath + "Prefabs");
            Directory.CreateDirectory(prjPath + "Materials");
            Directory.CreateDirectory(prjPath + "Resources");
            Directory.CreateDirectory(prjPath + "Scripts");
            Directory.CreateDirectory(prjPath + "Scenes");
            Directory.CreateDirectory(prjPath + "Fonts");
            if (1 == flag)
            {
                Directory.CreateDirectory(prjPath + "Editor");
                Directory.CreateDirectory(prjPath + "Plugins");
                Directory.CreateDirectory(prjPath + "StreamingAssets");
            }
            AssetDatabase.Refresh();
        } 
#endif
    }
}
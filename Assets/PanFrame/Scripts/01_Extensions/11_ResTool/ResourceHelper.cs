using System.Collections; 
using System.IO;
using UnityEngine;
namespace SQFramework
{
    public static class ResourceHelper
    {
        //Hashtable用来管理我们加载出来的资源的，作用就跟字典差不多
        private static Hashtable hashTexts = new Hashtable();
        private static Hashtable hashImages = new Hashtable();
        private static Hashtable hashPrefabs = new Hashtable();

        #region 加载资源
        /// <summary>
        /// 加载Prefab
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GameObject LoadPrefab(string path)
        {
            return LoadPfb(path);
        }
        /// <summary>
        /// 加载文本
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadText(string path)
        {
            return LoadTextFile(path, ".txt");
        }
        /// <summary>
        /// 加载XML文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadXML(string path)
        {
            return LoadTextFile(path, ".xml");
        }
        /// <summary>
        /// 加载图片文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string path)
        {
            return LoadTextrue(path);
        }
        public static Sprite LoadSprite(string path)
        {
            return LoadSpr(path);
        }
        #endregion
        private static string LoadTextFile(string path, string ext)
        {
            return LoadTextF(path, ext);
        }
        /// <summary>
        /// 加载预设体
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static GameObject LoadPfb(string path)
        {
            object obj = hashPrefabs[path];
            if (obj == null)
            {
                hashPrefabs.Remove(path);
                GameObject gameObject = Resources.Load(path) as GameObject;
                hashPrefabs.Add(path, gameObject);
                return gameObject;
            }

            return obj as GameObject;
        }

        /// <summary>
        /// 加载文本文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        private static string LoadTextF(string path, string ext)
        {
            object obj = hashTexts[path];
            if (obj == null)
            {
                hashTexts.Remove(path);
                string text = string.Empty;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                string pathstr = Application.dataPath + "/StreamingAssets/" + path + ext;
#else
			 string pathstr = Application.persistentDataPath + path + ext;
#endif

                text = File.ReadAllText(pathstr);

                hashTexts.Add(pathstr, ext);
                return text;
            }
            return obj as string;
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Texture2D LoadTextrue(string path)
        {
            object obj = hashImages[path];
            if (obj == null)
            {
                hashImages.Remove(path);
                Texture2D texture2D = (Texture2D)Resources.Load(path, typeof(Texture2D));
                hashImages.Add(path, texture2D);
                return texture2D;
            }
            return obj as Texture2D;
        }
        /// <summary>
        /// 加载精灵图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Sprite LoadSpr(string path)
        {
            object obj = hashImages[path];
            if (obj == null)
            {
                hashImages.Remove(path);
                Sprite sprite = (Sprite)Resources.Load(path, typeof(Sprite));
                hashImages.Add(path, sprite);
                return sprite;
            }
            return obj as Sprite;
        }
    }
}
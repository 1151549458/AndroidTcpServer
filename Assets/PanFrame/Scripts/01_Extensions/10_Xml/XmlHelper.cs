using System.Xml;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
namespace SQFramework
{
    public static class XmlHelper
    {
        #region XML相关
        /// <summary>
        /// 创建xml
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="root">跟节点</param>
        /// <param name="tag">标签</param>
        /// <param name="data">数据</param>
        public static void CreateXml(string filePath, string fileName, string root, string tag)
        {
            string strfile = filePath + "/" + fileName;
            CreateXml(strfile, root, tag);
        }
        public static void CreateXml(string file, string ro, string tag)
        {//创建路径 
            if (!File.Exists(file))
            {//没有就创建
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement(ro);
                XmlElement elmNew = xmlDoc.CreateElement(tag);
                root.AppendChild(elmNew);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(file);
                Debug.Log("CreatXml ok  fliePath " + file + " root " + ro + " 标签 " + tag);
            }
            else
            {
                Debug.Log("Xml existing");
            }
        }
        /// <summary>
        /// 创建Xml的Tag
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="tag"></param>
        public static void CreateXmlTag(string filePath, string fileName, string tag)
        {
            string strfile = filePath + "/" + fileName;
            CreateXmlTag(strfile, tag);
        }
        public static void CreateXmlTag(string file, string tag)
        {
            if (File.Exists(file))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file);
                XmlNode root = xmlDoc.DocumentElement;
                XmlElement elmNew = xmlDoc.CreateElement(tag);
                root.AppendChild(elmNew);
                xmlDoc.Save(file);
                Debug.Log("创建Tag成功");
            }
            else
            {
                Debug.Log("创建tag失败，文件不存在");
            }
        }
        /// <summary> 
        /// 获取xml属性
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="tag">标签</param>
        /// <param name="dataConst">某条key</param>
        /// <returns></returns>
        public static string GetXmlAttribute(string filepath, string fileName, string tag, string dataConst)
        {
            string strfile = filepath + "/" + fileName;
            return GetXmlAttribute(strfile, tag, dataConst);
        }
        public static string GetXmlAttribute(string strfile, string tag, string dataConst)
        {
            if (File.Exists(strfile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strfile);
                XmlNode root = xmlDoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;
                //遍历所有子节点
                foreach (XmlElement item in nodeList)
                {
                    if (item.Name == tag)
                    {
                        return item.GetAttribute(dataConst);
                    }
                }
            }
            return "";
        }
        // 如果想返回一条数据的话 ，就返回 XmlElement
        /// <summary>
        /// XML里这个属性是否存在这个值
        /// </summary>
        /// <param name="strfile"></param>
        /// <param name="dataConst"></param>
        /// <param name="dataValue"></param>
        /// <returns></returns>
        public static bool IsExistsXmlAttirbute(string strfile, string dataConst, string dataValue)
        {
            if (File.Exists(strfile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strfile);
                XmlNode root = xmlDoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;
                foreach (XmlElement item in nodeList)
                {
                    if (item.GetAttribute(dataConst) == dataValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static string GetTagByAttribute(string strfile, string dataConst, string dataValue)
        {
            if (File.Exists(strfile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strfile);
                XmlNode root = xmlDoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;
                foreach (XmlElement item in nodeList)
                {
                    if (item.GetAttribute(dataConst) == dataValue)
                    {
                        return item.Name;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 设置xml属性
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="tag">标签</param>
        /// <param name="dataConst">某条key</param>
        /// <param name="value">某条value</param>
        public static void SetXmlAttribute(string filepath, string fileName, string tag, string dataConst, string value)
        {
            string strfile = filepath + "/" + fileName;
            SetXmlAttribute(strfile, tag, dataConst, value);
        }
        public static void SetXmlAttribute(string strfile, string tag, string dataConst, string value)
        {
            if (File.Exists(strfile))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(strfile);
                XmlNode root = xmlDoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;
                //遍历所有子节点
                foreach (XmlElement item in nodeList)
                {
                    if (item.Name == tag)
                    {
                        item.SetAttribute(dataConst, value);
                    }
                }
                xmlDoc.Save(strfile);
            }
        }
        #endregion
    }
}
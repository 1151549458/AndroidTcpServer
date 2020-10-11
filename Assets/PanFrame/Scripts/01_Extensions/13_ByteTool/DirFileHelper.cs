using System;
using System.IO;
namespace SQFramework
{
    public static class DirFileHelper
    {
        #region 文件夹部分 
        /// <summary>
        /// 生成任何类型的文件
        /// </summary>
        /// <param name="fileBytes">文件流</param>
        /// <param name="path">当前文件的路径</param>
        /// <param name="folderName">文件夹</param>
        /// <param name="fileName">文件名带后缀</param>
        public static void SavaFile(byte[] fileBytes, string path, string folderName, string fileName)
        {
            FileStream fs = new FileStream(path + "/" + folderName + "/" + fileName, FileMode.Create, FileAccess.Write);
            fs.Write(fileBytes, 0, fileBytes.Length);
            fs.Dispose();
            fs.Close();
        }
        public static void SavaFile(byte[] fileBytes, string path, string fileName)
        {
            FileStream fs = new FileStream(path + "/" + fileName, FileMode.Create, FileAccess.Write);
            fs.Write(fileBytes, 0, fileBytes.Length);
            fs.Dispose();
            fs.Close();
        }
        public static void SavaFile(byte[] fileBytes, string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Dispose();
                fs.Close();
            }
        }
        /// <summary>
        /// 去读文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReadFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int dataLength = (int)fs.Length; //获得每个文件的大小
                byte[] buffer = new byte[dataLength];
                fs.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 创建一个文本文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void CreateFileTxt(string fileName, string content)
        {
            StreamWriter streamWriter = File.CreateText(fileName);
            streamWriter.Write(content);
            streamWriter.Close();
        }
        /// <summary>
        /// 创建一个文件夹
        /// </summary>
        /// <param name="fileName">这个fileName 是 path + id </param>
        public static void CreateDirectory(string fileName)
        {
            if (!IsExistsDirectory(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
        }
        /// <summary>
        /// 删除一个文件夹
        /// </summary>
        /// <param name="fileName"></param>
        public static void DeleteDirectory(string fileName)
        {
            if (IsExistsDirectory(fileName))
            {
                Directory.Delete(fileName, true);
            }
        }
        /// <summary>
        /// 获得当前文件夹下所有文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetAllDirectory(string path)
        {
            return Directory.GetDirectories(path);
        }
        /// <summary>
        /// 获取路径下所有的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetAllFile(string path)
        {
            return Directory.GetFiles(path);
        }
        /// <summary>
        /// 获取路径下某类文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern">*jpg</param>
        /// <returns></returns>
        public static string[] GetAllFile(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        } 
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsExistsFile(string fileName)
        {
            return File.Exists(fileName);
        }
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsExistsDirectory(string fileName)
        {
            return Directory.Exists(fileName);
        }
        #endregion

    }
}
/**
 *Copyright(C) 2018 by bw
 *All rights reserved.
 *FileName:     FtpHelper.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2019-12-24
 *Description:   
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.IO;
using System.Net;
using System.Text;
using System;
namespace SQFramework 
{
    public class FtpInfo
    {
        private static string FTPCONSTR = "ftp://47.75.218.219/";//FTP的服务器地址，格式为ftp://192.168.1.234/。ip地址和端口换成自己的，这些建议写在配置文件中，方便修改
        private static string FTPUSERNAME = "ftpAdm";//我的FTP服务器的用户名
        private static string FTPPASSWORD = "chinafoss";//我的FTP服务器的密码
    }

    public class FtpHelper
    {
        private static string FTPCONSTR = "ftp://39.105.5.69/";//FTP的服务器地址，格式为ftp://192.168.1.234/。ip地址和端口换成自己的，这些建议写在配置文件中，方便修改
        private static string FTPUSERNAME = "ftpAdm";//我的FTP服务器的用户名
        private static string FTPPASSWORD = "chinafoss";//我的FTP服务器的密码
        public static float uploadProgress;//上传进度
        public static float downloadProgress;//下载进度

        #region 本地文件上传到FTP服务器

        /// <summary>
        /// 文件上传到ftp
        /// </summary>
        /// <param name="ftpPath">ftp的文件路径</param>
        /// <param name="localPath">本地的文件目录</param>
        /// <param name="fileName">可重命名文件</param>
        /// <param name="pb">进度条</param>
        /// <returns></returns>
        public static bool UploadFiles(string ftpPath, string localPath, string fileName)
        {
            //path = "ftp://" + UserUtil.serverip + path;
            string erroinfo = "";
            float percent = 0;
            FileInfo f = new FileInfo(localPath);
            localPath = localPath.Replace("\\", "/");
            bool b = MakeDir(ftpPath);
            if (b == false)
            {
                return false;
            }
            localPath = FTPCONSTR + ftpPath + fileName;
            FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(localPath));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.ContentLength = f.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = f.OpenRead();
            int allbye = (int)f.Length;

            int startbye = 0;
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    startbye = contentLen + startbye;
                    percent = (float)startbye / (float)allbye * 100;
                    if (percent <= 100)
                    {
                        //Debug.Log(percent);
                        uploadProgress = percent;
                    }

                    contentLen = fs.Read(buff, 0, buffLength);

                }
                strm.Close();
                fs.Close();
                erroinfo = "完成";
                return true;
            }
            catch (Exception ex)
            {
                erroinfo = string.Format("因{0},无法完成上传", ex.Message);
                return false;
            }
        }



        #endregion

        #region 从ftp服务器下载文件
         
        /// 从ftp服务器下载文件的功能----带进度条
        /// </summary>
        /// <param name="ftpfilepath">ftp下载的地址</param>
        /// <param name="filePath">保存本地的地址</param>
        /// <param name="fileName">保存的名字</param>
        /// <param name="pb">进度条引用</param>
        /// <returns></returns>
        public static bool Download(string ftpfilepath, string filePath, string fileName)
        {
            FtpWebRequest reqFtp = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;
            FileStream outputStream = null;
            try
            {
                filePath = filePath.Replace("我的电脑\\", "");
                String onlyFileName = Path.GetFileName(fileName);
                string newFileName = filePath + onlyFileName;
                ftpfilepath = ftpfilepath.Replace("\\", "/");
                string url = FTPCONSTR + ftpfilepath;
                reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                response = (FtpWebResponse)reqFtp.GetResponse();
                ftpStream = response.GetResponseStream();
                long cl = GetFileSize(url);

                if (File.Exists(newFileName))
                { 
                    try
                    {
                        //判断文件的长度如果长度不一样就删了 否则不做事情
                        FileInfo fileInfo = new FileInfo(newFileName);
                        if (fileInfo.Length != cl)
                        {
                            File.Delete(newFileName);
                        }
                        else
                        {
                            downloadProgress = 1; 
                            return true;
                        }
                         
                    }
                    catch { }

                } 
                int bufferSize = 1024*1024;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                outputStream = new FileStream(newFileName, FileMode.Create);

                float percent = 0;
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    percent = (float)outputStream.Length / (float)cl ;
                    if (percent <= 100)
                    {
                        // Debug.Log(percent);
                        downloadProgress = percent;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("因{0},无法下载" + ex.Message);
                return false;
            }
            finally
            {
                if (reqFtp != null)
                    reqFtp.Abort();
                if (response != null)
                    response.Close();
                if (ftpStream != null)
                    ftpStream.Close();
                if (outputStream != null)
                    outputStream.Close();
            }
        }
        #endregion
        /// <summary>
        /// 判断断点续传
        /// </summary>
        /// <param name="fileName">本地文件</param>
        /// <param name="serverUri">服务器路径</param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static bool RestartDownloadFromServer(string fileName, Uri serverUri, long offset)
        {
            // The serverUri parameter should use the ftp:// scheme.
            // It identifies the server file that is to be downloaded
            // Example: ftp://contoso.com/someFile.txt.

            // The fileName parameter identifies the local file.
            //The serverUri parameter identifies the remote file.
            // The offset parameter specifies where in the server file to start reading data.

            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return false;
            }
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.ContentOffset = offset;
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Status);
                Console.WriteLine(e.Message);
                return false;
            }
            // Get the data stream from the response.
            Stream newFile = response.GetResponseStream();
            // Use a StreamReader to simplify reading the response data.
            StreamReader reader = new StreamReader(newFile);
            string newFileData = reader.ReadToEnd();
            // Append the response data to the local file
            // using a StreamWriter.
            StreamWriter writer = File.AppendText(fileName);
            writer.Write(newFileData);
            // Display the status description.

            // Cleanup.
            writer.Close();
            reader.Close();
            response.Close();
            Console.WriteLine("Download restart - status: {0}", response.StatusDescription);
            return true;
        }



        #region 获得文件的大小
        /// <summary>
        /// 获得文件大小
        /// </summary>
        /// <param name="url">FTP文件的完全路径</param>
        /// <returns></returns>
        public static long GetFileSize(string url)
        {

            long fileSize = 0;
            try
            {
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                fileSize = response.ContentLength;

                response.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            return fileSize;
        }
        #endregion

        #region 在ftp服务器上创建文件目录

        /// <summary>
        ///在ftp服务器上创建文件目录
        /// </summary>
        /// <param name="dirName">文件目录</param>
        /// <returns></returns>
        public static bool MakeDir(string dirName)
        {
            try
            {
                string uri = (FTPCONSTR + dirName + "/");
                if (DirectoryIsExist(uri))
                {
                    Debug.Log("已存在");
                    return true;
                }

                string url = FTPCONSTR + dirName;
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                // reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("因{0},无法下载" + ex.Message);
                return false;
            }

        }
        /// <summary>
        /// 判断ftp上的文件目录是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>        
        public static bool DirectoryIsExist(string uri)
        {
            string[] value = GetFileList(uri);
            if (value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private static string[] GetFileList(string uri)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(uri);
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 从ftp服务器删除文件的功能
        /// <summary>
        /// 从ftp服务器删除文件的功能
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool DeleteFtpFile(string fileName)
        {
            try
            {
                string url = FTPCONSTR + fileName;
                FtpWebRequest reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFtp.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                //errorinfo = string.Format("因{0},无法下载", ex.Message);
                return false;
            }
        }
        #endregion

        #region  从ftp服务器上获得文件夹列表
        /// <summary>
        /// 从ftp服务器上获得文件夹列表
        /// </summary>
        /// <param name="RequedstPath">服务器下的相对路径</param>
        /// <returns></returns>
        public static List<string> GetFtpDirctory(string RequedstPath)
        {
            List<string> strs = new List<string>();
            try
            {
                string uri = FTPCONSTR + RequedstPath;   //根路径+路径
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                // ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());//中文文件名

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Contains("<DIR>"))
                    {
                        string msg = line.Substring(line.LastIndexOf("<DIR>") + 5).Trim();
                        strs.Add(msg);
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return strs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取目录出错：" + ex.Message);
            }
            return strs;
        }
        #endregion

        #region 从ftp服务器上获得文件列表
        /// <summary>
        /// 从ftp服务器上获得文件列表
        /// </summary>
        /// <param name="RequedstPath">服务器下的相对路径</param>
        /// <returns></returns>
        public static List<string> GetFtpFiles(string RequedstPath)
        {
            List<string> strs = new List<string>();
            try
            {
                string uri = FTPCONSTR + RequedstPath;   //根路径+路径
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                // ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(FTPUSERNAME, FTPPASSWORD);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());//中文文件名

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (!line.Contains("<DIR>"))
                    {
                        string msg = line.Substring(39).Trim();
                        strs.Add(msg);
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return strs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取文件出错：" + ex.Message);
            }
            return strs;
        }

        #endregion
    }
}
 
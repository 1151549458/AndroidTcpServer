using System;
using System.Text;
using UnityEngine;
namespace SQFramework
{ 

    public static class BytesHelper
    {
        #region Byte相关
        /// <summary>
        /// 拷贝数据
        /// </summary>
        /// <param name="src">要拷贝的数组</param>
        /// <param name="srcOffset">要拷贝数据的第几位开始</param>
        /// <param name="dst">要存储的数据</param>
        /// <param name="dstOffset">要存储数据的第几位开始</param>
        /// <param name="count">存多少长度</param>
        public static void CopyArray(Array src, int srcOffset, Array dst, int dstOffset, int count)
        {
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }
        //public static void CopyArray(Array src, int srcOffset, Array dst, int dstOffset, int count)
        //{
        //    Array.Copy(src, srcOffset, dst, dstOffset, count);
        //}

        /// <summary>
        /// byte[] 转成str
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string PressStr(byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// str转成Byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] PressByte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        /// <summary>
        /// Texture的转换
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Texture2D TextureToTexture2D(Texture texture)
        {//Texture转成Texture2d
            Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
            Graphics.Blit(texture, renderTexture);
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();//像素信息保存在2D纹理贴图中;
            RenderTexture.active = currentRT;
            RenderTexture.ReleaseTemporary(renderTexture);
            return texture2D;
        }


        #endregion

    }
}
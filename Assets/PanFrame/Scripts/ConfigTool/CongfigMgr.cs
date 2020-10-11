
/*
 *Copyright(C)   #DATE#
 *All rights reserved.
 *ProductName:  #PRODUCTNAME#
 *FileName:     #SCRIPTFULLNAME#
 *Author:       #AUTHOR#
 *Version:      #VERSION#
 *UnityVersion：#UNITYVERSION#
 *CreateTime:   #CreateTime#
 *Description:   
 *History:*/
using SharpConfig; 
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace SQFramework
{
    public class ConfigMgrExample : MonoBehaviour
    {
        void Start()
        {
            string st1 = CongfigMgr.Instance().GetConfig().Get(Constant.CONFIG_DIR_NAME);
            Debug.Log("读取Config");
        }

    }


    public class CongfigMgr :MonoSingleton<CongfigMgr>
    {

        //private static CongfigMgr s_instance;
        //public static CongfigMgr Instance
        //{
        //    get
        //    {
        //        if (s_instance == null)
        //        {
        //            s_instance = new CongfigMgr();
        //            Debug.Log("在场景中没有这一实例,我创建了一个");
        //        }
        //        return s_instance;
        //    }
        //}
        #region 实例化config
        private Config config;
        public Config GetConfig()
        {
            if (config == null)
            {
                string configPath;
#if UNITY_EDITOR
                configPath = Path.Combine(Application.dataPath, Constant.CONFIG_DIR_NAME);
#elif UNITY_STANDALONE_WIN
            configPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), Constant.CONFIG_DIR_NAME);
#endif
                configPath = Path.Combine(configPath, Constant.CONFIG_FILE_NAME);
                config = new Config(configPath);
            }
            return config;
        }
        #endregion

    }

    public class Config
    {

        private Dictionary<string, string> datas;
        public Config(string filePath)
        {
            Configuration config = Configuration.LoadFromFile(filePath);
            datas = new Dictionary<string, string>();
            foreach (Section section in config)
            {
                foreach (Setting setting in section)
                {
                    datas.Add(setting.Name, setting.StringValue);
                }
            }
        }

        public string Get(string key)
        {
            if (datas.ContainsKey(key))
            {
                return datas[key];
            }
            return null;
        }
    }









}
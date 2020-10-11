/**
 *Copyright(C) 2018 by DefaultCompany
 *All rights reserved.
 *FileName:     MathUtil.cs
 *Author:       Pan
 *Version:      1.0
 *UnityVersion��2018.4.13f1
 *Date:         2020-05-18
 *Description:   
 *History:
*/

using UnityEngine;

namespace SQFramework
{

    public class MathUtilExample:MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(MathUtil.GetRandomValueFrom(1, 2, 3));
            Debug.Log(MathUtil.GetRandomValueFrom("asdasd", "123123"));
            Debug.Log(MathUtil.GetRandomValueFrom(0.2f));
            Debug.Log(MathUtil.Percent(50)); //代表50%
        }
    } 

    /// <summary>
    /// 数学方法
    /// </summary>
    public static partial class MathUtil
    {
        //Debug.Log(Percent(50)); //代表50%

        /// <summary>
        /// 输入百分比返回是否命中概率
        /// </summary>
        public static bool Percent(int percent)
        {
            return Random.Range(0, 100) <= percent;
        }

        /// <summary>
        /// 从表里随机取一个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T GetRandomValueFrom<T>(params T[] values) 
        {//params 表示可以传多个参数，用于数组
            return values[Random.Range(0, values.Length)];
        }
        public static int GetRandomValueFrom(int index)
        {//params 表示可以传多个参数，用于数组
            return Random.Range(0, index);
        }
        public static int GetRandomValue(int start, int end)
        {//params 表示可以传多个参数，用于数组
            return Random.Range(start, end);
        }
        public static float GetRandomValue(float start, float end)
        {
            return Random.Range(start,end);
        } 
        public static Vector3 GetRandomValue(Vector3 start, Vector3 end)
        {//params 表示可以传多个参数，用于数组
            return new Vector3(GetRandomValue(start.x, end.x), 
                GetRandomValue(start.y, end.y), GetRandomValue(start.z, end.z)); 
        }
        public static Vector2 GetRandomValue(Vector2 start, Vector2 end)
        {//params 表示可以传多个参数，用于数组
            return new Vector2(GetRandomValue(start.x, end.x),
                GetRandomValue(start.y, end.y));
        }


    }
}
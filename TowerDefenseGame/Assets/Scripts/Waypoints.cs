using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour //为了创建enemy路线而设立检查点
{
    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount]; //初始化数组的长度
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i); //给数组赋值

        }
    }
}

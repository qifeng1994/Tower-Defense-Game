using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour //这个脚本用于让enemy沿着固定的路线移动
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position; //dir是enemy移动的方向向量
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //把向量模取1然后乘时间来计算位移

        if (Vector3.Distance(transform.position,target.position) <= 0.4f)
        {
            GetNextPoint(); //如果enemy移动到了检查点，则更新下一个检查点   
        }
    }

    void GetNextPoint()
    {
        if (wavepointIndex>=Waypoints.points.Length-1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}

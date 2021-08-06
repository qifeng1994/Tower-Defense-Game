using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour //这个脚本用于让enemy沿着固定的路线移动
{
    public float speed = 10f;
    public int value = 50;
    public int health = 100;

    private Transform target;
    private int wavepointIndex = 0;

    private void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamage(int amount) //敌人被子弹击中时掉血
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStatus.Money += value; //敌人死亡时玩家获得金钱奖励
        Destroy(gameObject);
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
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStatus.Lives--; //敌人到终点时，玩家生命值减一
        Destroy(gameObject);
    }
}

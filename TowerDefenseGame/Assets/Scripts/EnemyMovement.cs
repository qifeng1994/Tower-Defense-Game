using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] //
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position; //dir是enemy移动的方向向量
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); //把向量模取1然后乘时间来计算位移

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextPoint(); //如果enemy移动到了检查点，则更新下一个检查点   
        }

        enemy.speed = enemy.startSpeed; //这句话是为了让当enemy在laser的射程外时，enemy的速度变回正常
    }

    void GetNextPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
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

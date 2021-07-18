using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f; //炮台的锁敌范围
    public float turnSpeed = 10; //炮台转向的速度

    public string enemyTag = "Enemy";

    public Transform partToRotate; //炮台需要转动的是上半部分

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //0秒后调用函数，之后每0.5秒调用一次
    }

    void UpdateTarget() //
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); 
        float shortestDistance = Mathf.Infinity; //初始值为无穷大
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) //找到最近的enemy
        {
            float distanceToEnemy = Vector3.Distance(transform.position , enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy!=null&&shortestDistance<=range) //如果最近的enemy在锁敌范围内，则把enemy赋值给target
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position; //dir是turret指向最近的enemy的向量
        Quaternion lookRatation = Quaternion.LookRotation(dir); //
        // Vector3 rotation = lookRatation.eulerAngles; //以欧拉角来表示旋转
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //绕y轴旋转
    }


    private void OnDrawGizmosSelected() //把turret的锁敌范围绘制出来,当被选择时才会出现
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("General")]
    public float range = 15f; //炮台的锁敌范围

    [Header("Use Bullets(default)")]
    public float fireRate = 1f; //每秒发射一次
    private float fireCountdown = 0f; //每次射击间隔的时间
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer; //

    [Header("Unity Setup Fields")]
    public float turnSpeed = 10; //炮台转向的速度
    public string enemyTag = "Enemy";
    public Transform partToRotate; //炮台需要转动的是上半部分

    public Transform firePoint;

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
        if (target == null) //当敌人在射程外时，不发射子弹或者射线
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                    lineRenderer.enabled = false;
            }
            return;
        }

        LockOnTarget(); //

        if(useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }


    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //子弹实例化
        Bullet bullet = bulletGo.GetComponent<Bullet>(); //通过这样的方式可以确定子弹的位置信息

        if (bullet != null)
            bullet.Seek(target); //把enemy的信息转递给bullet类

    }


    private void OnDrawGizmosSelected() //把turret的锁敌范围绘制出来,当被选择时才会出现
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position; //dir是turret指向最近的enemy的向量
        Quaternion lookRatation = Quaternion.LookRotation(dir); //
        // Vector3 rotation = lookRatation.eulerAngles; //以欧拉角来表示旋转
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //绕y轴旋转
    }

    void Laser()
    {
        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, firePoint.position); //线的起点为炮塔的炮口
        lineRenderer.SetPosition(1, target.position); //线的终点为敌人的坐标
    }
}

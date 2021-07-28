using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public GameObject impactEffect;

    public float speed = 70f;

    public void Seek(Transform _target) //希望把一个来自其他类的transform赋值给这里的target
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) //当向量的长度接近要移动的距离时，说明子弹打到了敌人
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //位移
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation); //
        Destroy(effectIns, 2f);

        Destroy(target.gameObject);

        Destroy(gameObject);
    }

}

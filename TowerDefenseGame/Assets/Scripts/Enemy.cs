using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour //这个脚本用于让enemy沿着固定的路线移动
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public int worth = 50;
    public float health = 100;

    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount) //敌人被子弹击中时掉血
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStatus.Money += worth; //敌人死亡时玩家获得金钱奖励
        Destroy(gameObject);
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
}

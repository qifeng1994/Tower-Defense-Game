using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour //每5秒出现一波敌人，每波敌人数量加一
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f; //游戏开始时2秒倒计时
    private int waveIndex = 0;

    public Text waveCountdownText; //添加一个UI

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave()); 
            countdown = timeBetweenWaves; //每过5秒调用一次协同程序
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        //waveCountdownText.text = Mathf.Round(countdown).ToString(); //倒计时的数字取整并转换成字符串赋值给UI

        waveCountdownText.text = string.Format("{0:00.00}", countdown); //{0:00.00}的写法有什么依据
    }

    IEnumerator SpawnWave() //协同程序
    {
        waveIndex++;
        PlayerStatus.Rounds++; //用于记录玩家的回合数

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

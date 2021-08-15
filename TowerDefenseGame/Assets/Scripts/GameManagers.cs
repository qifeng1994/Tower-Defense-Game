using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour //用于管理游戏的开始结束保存等等
{
    public static bool GameIsOver;

    public GameObject gameOverUI;

    private void Start()
    {
        GameIsOver = false;
    }
    private void Update()
    {
        if (GameIsOver)
            return;

        if(PlayerStatus.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true); //激活游戏结束的界面
        Debug.Log("Game Over.");
    }
}

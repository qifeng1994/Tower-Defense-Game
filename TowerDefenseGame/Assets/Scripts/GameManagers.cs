using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour //用于管理游戏的开始结束保存等等
{
    private bool gameEnded = false;
    private void Update()
    {
        if (gameEnded)
            return;

        if(PlayerStatus.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over.");
    }
}

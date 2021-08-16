using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle() //切换功能：时间暂停显示UI或者时间恢复隐藏UI
    {
        ui.SetActive(!ui.activeSelf); //

        if(ui.activeSelf)
        {
            Time.timeScale = 0f; //相当于时间齿轮 实现暂停游戏
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour 
{
    public Text livesText;

    private void Update()
    {
        livesText.text = PlayerStatus.Lives.ToString() + " LIVES"; //把玩家的生命数显示到canvasUI上面
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public GameObject Gameovercanvas;

    float setTime = 180;

    public bool isGameover = false;

    int min;
    float sec;

    void Update()
    {
        setTime -= Time.deltaTime;

        if (setTime >= 60f)
        {
            min = (int)setTime / 60;
            sec = setTime % 60;
            timeText.text = "Time " + min + ":" + (int)sec;
        }

        if (setTime < 60f)
        {
            timeText.text = "Time " + (int)setTime;
        }

        if (setTime <= 0)
        {
            timeText.text = "Time : 0";
            EndGame();
        }
    }

    public void EndGame()
    {
        isGameover = true;
        Gameovercanvas.SetActive(true);

        if (Scoreboard.instance != null)
        {
            Scoreboard.instance.canvasGroup.alpha = 1;
        }
    }
}

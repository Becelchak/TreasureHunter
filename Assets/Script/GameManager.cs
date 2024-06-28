using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMain;
    [SerializeField]
    private double timer = 120;

    void Update()
    {
        Countdown();
    }

    void Countdown()
    {
        timer -= Time.deltaTime;
        textMain.text = $"Осталось {Math.Round(timer)} секунд";
    }

    public void AddTime(double time)
    {
        timer += time;
    }
}

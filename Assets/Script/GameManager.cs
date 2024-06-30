using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMain;
    [SerializeField]
    private double timer = 120;
    [SerializeField]
    private CanvasGroup startPage;
    public bool tutorialEnd;

    void Update()
    {
        if (!tutorialEnd)
        {
            startPage.alpha = 1.0f;
            startPage.interactable = true;
            startPage.blocksRaycasts = true;
        }
        else
        {
            Countdown();
        }
       
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

    public void RemoveTime(double time)
    {
        timer -= time;
    }

    public void TutorialEnd() 
    {
        tutorialEnd = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

}

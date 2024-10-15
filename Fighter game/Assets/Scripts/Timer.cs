using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText; //The text that displays the timer
    public TextMeshProUGUI ScoreText;
    public float CurrentTime; //The Timer
    public float FinishTime; //The finish Time
    public float Score = 1000;
    public GameObject OneStar;
    public GameObject TwoStar;
    public GameObject ThreeStar;


    void Update()
    {
        CurrentTime += Time.deltaTime; //The timer counting
        TimerText.text = ("Time: " + CurrentTime.ToString("0.00")); //displays the time
    }
    public void EndTimer()
    {
        FinishTime = CurrentTime;
        Score = Score / FinishTime;
        Score = SettingsMenu.DifficultyLevel * SettingsMenu.EnemyAmount * Score;
        ScoreText.text = Score.ToString("0");

        if(Score < 250)
        {
            OneStar.SetActive(true);
        }
        else if(Score >=250 && Score < 500)
        {
            TwoStar.SetActive(true);
        }
        else
        {
            ThreeStar.SetActive(true);
        }
    }
}
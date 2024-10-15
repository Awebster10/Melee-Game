using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public static int DifficultyLevel = 1;
    public static int EnemyAmount = 1;
    public void SetDifficulty(float Difficulty)
    {
        DifficultyLevel = (int)Difficulty;
    }
    public void SetEnemies(float EnemyCount)
    {
        EnemyAmount = (int)EnemyCount;
    }
    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}

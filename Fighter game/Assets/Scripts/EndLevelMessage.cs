using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMessage : MonoBehaviour
{
    public void TryAgainButton()
    {
        SceneManager.LoadScene(2);
    }
    public void Settings()
    {
        SceneManager.LoadScene(1);
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}

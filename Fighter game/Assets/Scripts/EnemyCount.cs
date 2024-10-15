using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public int DeadEnemies;
    public HeroKnight heroKnight;
    public int Temp;
    int Enemynum = SettingsMenu.EnemyAmount;
    // Start is called before the first frame update
    void Start()
    {
        if(SettingsMenu.EnemyAmount == 0)
        {
            SettingsMenu.EnemyAmount = 1;
        }
        switch(SettingsMenu.EnemyAmount)
        {
            case 2:
            Enemy2.SetActive(true);
            break;

            case 3:
            Enemy2.SetActive(true);
            Enemy3.SetActive(true);
            break;

            case 4:
            Enemy2.SetActive(true);
            Enemy3.SetActive(true);
            Enemy4.SetActive(true);
            break;
        }
    }

    public void CheckEnemies()
    {
        if(DeadEnemies == Enemynum)
        {
            heroKnight.GetComponent<HeroKnight>().PlayerWins();
        }
    }

}

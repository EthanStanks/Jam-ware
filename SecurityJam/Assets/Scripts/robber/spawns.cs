using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawns : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRobber()
    {
        GameObject Robber = Instantiate(GameManager.instance.RobberPrefab, transform.position, transform.rotation);
        Robber.name = "Robber";
        Robber.GetComponent<theft>().isCaught = false;
        GameManager.instance.lstRobbers.Add(Robber);
    }

    //example code for spawning
    /*
    GameObject enemy = Instantiate(GameManager.Instance.towerKillerEnemyPrefab, transform.position, transform.rotation);
    enemy.name = "Tower Killer Enemy";
        enemy.GetComponent<BasicEnemyScript>().mEnemySpeed = 3.5f;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
        if (GameManager.Instance.currentWeatherScript.mWeatherEventIndex == 5) // checks for snow
        {
            enemy.GetComponent<BasicEnemyScript>().SnowEffect();
    enemy.GetComponent<BasicEnemyScript>().mIsEffectedBySnow = true;
        }
    GameManager.Instance.enemyCount++;
    */
}

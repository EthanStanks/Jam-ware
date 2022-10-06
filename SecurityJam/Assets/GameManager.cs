using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject PlayerPrefab; // player obj
    [SerializeField] public GameObject RobberPrefab; //robber obj
    public List<GameObject> lstRobbers; // list of robbers obj
    public List<GameObject> lstEntrances; // list of entrance obj
    public List<GameObject> lstSecurityCameras; // list of camera obj
    int PlayerLives = 1;
    int intCurrentMap; // current map int
    int timer;
    bool blTimerEnded; // timer ended bool


    // start game func
    private void Start()
    {
        timer = 0;
        blTimerEnded = false;
    }

    private void Update()
    {
        if (timer % 5 == 0)
        {
            SpawnRobber();
        }
        if (PlayerLives == 0)
        {
            LostGame();
        }
        if (blTimerEnded == true)
        {
            WonGame();
        }
    }

    // end game func
    void EndGame()
    {
        Debug.Log("Game Over");
    }

    IEnumerator OneSecBruh(int num)
    {
        yield return new WaitForSeconds(1);
        num++;
    }

    // start game timer func
    void StartGameTimer()
    {
        while (blTimerEnded == false)
        {
            OneSecBruh(timer);
        }
    }

    // won game func
    void WonGame()
    {
        Debug.Log("You win");
        EndGame();
    }
    // lost game func
    void LostGame()
    {
        Debug.Log("You suck");
        EndGame();
    }
    // escaped func
    public void RobberEscaped()
    {
        PlayerLives--;// a life system for big maps
    }

    // spawn robber
    void SpawnRobber()
    {
        System.Random rand = new System.Random();
        int num = rand.Next(lstEntrances.Count);
        lstEntrances[num].GetComponent<EntranceController>().SpawnRobber();
    }
}


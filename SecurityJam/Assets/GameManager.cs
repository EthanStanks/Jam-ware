using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject PlayerPrefab; // player obj
    [SerializeField] public GameObject RobberPrefab; //robber obj
    public List<GameObject> lstRobbers = new(); // list of robbers obj
    public List<GameObject> lstEntrances = new(); // list of entrance obj
    public List<GameObject> lstSecurityCameras = new(); // list of camera obj
    [SerializeField] int PlayerLives = 1;
    int intCurrentMap; // current map int
    [SerializeField] public int timer;
    bool blTimerEnded; // timer ended bool
    bool timing = false;
    bool canSpawn = false;

    public void Awake()
    {
        instance = this;
    }

    // start game func
    private void Start()
    {
        timer = 0;
        blTimerEnded = false;
        timing = true;
    }

    private void Update()
    {
        if (timing)
        {
            timing = false;
            StartCoroutine(OneSecBruh());
        }
        if (timer % 5 == 0 && timer != 0 && canSpawn)
        {
            canSpawn = false;
            SpawnRobber();
        }
        if (timer % 5 == 1)
        {
            canSpawn = true;
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

    IEnumerator OneSecBruh()
    {
        yield return new WaitForSeconds(1);
        timer++;
        timing = true;
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
        if (num == 0)
        {
            SpawnRobber();
        }
        lstEntrances[num].GetComponent<EntranceController>().spawnRobber = true;
    }
}


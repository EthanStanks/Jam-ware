using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject PlayerPrefab; // player obj
    [SerializeField] public GameObject RobberPrefab; //robber obj
    public List<GameObject> lstRobbers = new(); // list of robbers obj
    public List<GameObject> lstEntrances = new(); // list of entrance obj
    public List<GameObject> lstSecurityCameras = new(); // list of camera obj
    [SerializeField] public int PlayerLives = 1;
    [SerializeField] public int GameEnd = 120;
    int intCurrentMap; // current map int
    [SerializeField] public int timer;
    //bool blTimerEnded; // timer ended bool
    bool timing = false;
    bool canSpawn = false;
    [SerializeField] public bool isPaused = false;
    [SerializeField] GameObject LoseScreen;

    [SerializeField] LevelLoader LevelLoaderScript; // i need it i neeeeeeeeeeeeeeeeed it
    [SerializeField] GameObject PauseScreen;

    [SerializeField] Sprite[] clock;
    [SerializeField] Image clockObj;

    public void Awake()
    {
        instance = this;
    }

    // start game func
    private void Start()
    {
        timer = 0;
        //blTimerEnded = false;
        timing = true;
        PauseGame(isPaused);
        UpdateClock();
    }

    private void Update()
    {
        KeyBindings();

        if (timing)
        {
            timing = false;
            StartCoroutine(OneSecBruh());
        }
        if (timer % 28 == 0 && timer != 0 && canSpawn)
        {
            canSpawn = false;
            SpawnRobber();
        }
        if (timer % 28 == 1)
        {
            canSpawn = true;
        }
        if (PlayerLives == 0)
        {
            LostGame();
        }
        if (timer > 120)
        {
            WonGame();
        }
        if (timer % 10 == 0 && timer > 5)
        {
            UpdateClock();
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
        LoseScreen.SetActive(true);
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

    public void PauseGame(bool isPause)
    {
        PauseScreen.SetActive(isPause);
        isPaused = isPause;
        if (isPause)
        {
            Time.timeScale = 0;
           // anything else that needs to happen when the game is paused
        }
        else
        {
            Time.timeScale = 1;
            // anything else that needs to happen when the game is resumed
        }
    }
    public void ReturnToMainMenu()
    {
        PauseGame(false);
        // make add stuff that would make it that they lost or gave up idk
        LevelLoaderScript.LoadLevel(0); // loads the main menu
    }
    void KeyBindings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!isPaused);
        }
            
    }

    void UpdateClock()
    {
        int hour = GameEnd / 6;
        if (timer < hour)//midnight
        {
            clockObj.sprite = clock[1];
        }
        else if (timer < hour * 2)
        {
            clockObj.sprite = clock[2];
        }
        else if (timer < hour * 3)
        {
            clockObj.sprite = clock[3];
        }
        else if (timer < hour * 4)
        {
            clockObj.sprite = clock[4];
        }
        else if (timer < hour * 5)
        {
            clockObj.sprite = clock[5];
        }
        else if (timer < GameEnd)
        {
            clockObj.sprite = clock[6];
        }
        else if (timer > GameEnd)
        {
            clockObj.sprite = clock[7];
        }
        else
        {
            clockObj.sprite = clock[0];
        }
    }

}


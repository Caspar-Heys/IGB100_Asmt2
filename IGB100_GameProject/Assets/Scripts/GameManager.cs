using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float startTime;
    public GameObject level01Prefeb;
    private GameObject level01;
    //public float maxTime = 15.0f;
    //Singleton Setup
    public static GameManager instance = null;

    public GameObject player;
    public GameObject playerStartPosition;
    public GameObject mainCamera;
    public GameObject uiController;

    public bool win = false;
    public bool gameOver = false;
    public bool menu = true;
    public bool pause = false;

    public int token = 0;
    public int score = 0;
    
    //private float pauseDelay = 0.5f;
    //private float pauseTimer = 0.0f;



    // Awake Checks - Singleton setup
    void Awake()
    {

        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        mainCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;
        
        
    }

    public void StartLevel()
    { 
        gameOver = false;
        win = false;
        mainCamera.SetActive(false);
        player.SetActive(true);
        player.transform.position = playerStartPosition.transform.position;
        player.GetComponent<Player>().health = 100;
        //player.GetComponent<Player>().killIntention = 0;
        level01 = Instantiate(level01Prefeb, transform.position, transform.rotation);
        uiController.GetComponent<UIController>().ShowPlayerUI();
        menu = false;
        startTime = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        gameOver = true;
        Destroy(level01);
        player.SetActive(false);
        mainCamera.SetActive(true);
        if (win)
        {
            uiController.GetComponent<UIController>().ShowWinUI();
        }
        else
        {
            uiController.GetComponent<UIController>().ShowLoseUI();

        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


    }
   
}
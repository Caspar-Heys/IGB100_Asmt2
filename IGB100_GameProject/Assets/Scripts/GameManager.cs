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
    public GameObject uiController;

    public bool win = false;
    public bool gameOver = false;
    public bool menu = false;
    public bool pause = false;
    public bool hasPlayed = false;

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
        Time.timeScale = 1;
        Debug.Log("game start");
        level01 = Instantiate(level01Prefeb, transform.position, transform.rotation);
        startTime = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<Player>().health = 100;
        gameOver = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;

        
    }


    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = false;
        GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = false;
        //Destroy(level01);
        //player.SetActive(false);
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
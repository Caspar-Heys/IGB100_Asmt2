using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        //GameObject.FindWithTag("Player").GetComponent<Player>().health = 100;
        //tokens = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().token;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

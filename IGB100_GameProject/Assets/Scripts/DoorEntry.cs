using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEntry : MonoBehaviour, IInteractable
{
    public GameObject DoorUI;
    public void Interact()
    {
        DoorUI.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = false;
        //GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = false;
        GameObject.FindWithTag("Player").GetComponentInChildren<ShotGun>().enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (DoorUI.activeInHierarchy)
                DontStartGame();
        }
    }
    public void StartGame()//player chooses to start game
    {
        SceneManager.LoadScene("Scene01");
    }
    public void DontStartGame()//player choose not to start game{
    {
        DoorUI.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = true;
        //GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = true;
        GameObject.FindWithTag("Player").GetComponentInChildren<ShotGun>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

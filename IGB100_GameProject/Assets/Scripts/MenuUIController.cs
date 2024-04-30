using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject instructionsUI;
    public GameObject optionsUI;
    // Start is called before the first frame update
    void Start()
    {
        BackToMenu();   
    }


    public void ShowInstructionsUI()
    {
        menuUI.SetActive(false);
        instructionsUI.SetActive(true);
    }
    public void ShowOptionsUI()
    {
        menuUI.SetActive(false);
        optionsUI.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scene01");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        instructionsUI.SetActive(false);
        optionsUI.SetActive(false);
        menuUI.SetActive(true);
    }
}

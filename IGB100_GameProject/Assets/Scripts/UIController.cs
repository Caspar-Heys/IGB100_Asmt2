using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject instructionsUI;
    public GameObject optionsUI;
    public GameObject InteractPrompt;
    public GameObject playerUI;
    public Slider healthBar;
    public Slider killIntentionBar;
    public Slider magazineBar;
    public TMP_Text hpTxt;
    public TMP_Text kiTxt;
    public GameObject pressF;
    public TMP_Text magazineTxt;

    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject loseUI;

    public TMP_Text fpsUI;

    private int fps;
    private float fpsDisplayCD = 1.0f;
    private float fpsDisplayTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(true);
        instructionsUI.SetActive(false);
        playerUI.SetActive(false);
        optionsUI.SetActive(false);
        pauseUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.menu && !GameManager.instance.pause && (Input.GetKey("p")|| Input.GetKey("escape")))
        {
            PauseGame();
            // show pause ui and cursor
            
        }
        if (Time.time > fpsDisplayTimer)
        {
            fps = Mathf.FloorToInt(1 / Time.deltaTime);
            fpsUI.text = "FPS: " + fps.ToString();
            fpsDisplayTimer = Time.time + fpsDisplayCD;
        }
        
    }

    
    public void BackToMenu()
    {
        menuUI.SetActive(true);
        instructionsUI.SetActive(false);
        optionsUI.SetActive(false);
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
    public void ShowPlayerUI()
    {
        playerUI.SetActive(true);
        menuUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        GameManager.instance.pause = true;
        pauseUI.SetActive(true);
        GameManager.instance.player.SetActive(false);
        GameManager.instance.mainCamera.SetActive(true);
        playerUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        GameManager.instance.pause = false;
        pauseUI.SetActive(false);
        GameManager.instance.mainCamera.SetActive(false);
        GameManager.instance.player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        playerUI.SetActive(true);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Scene01");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ShowWinUI()
    {
        playerUI.SetActive(false);
        winUI.SetActive(true);
    }
    public void ShowLoseUI() 
    {
        playerUI.SetActive(false);
        loseUI.SetActive(true);
    }

    public void UpdateHpBar(float hp, float maxHp)
    {
        healthBar.value = hp / maxHp;
        hpTxt.text = "HP: " + hp + " / " + maxHp;
    }
    public void UpdateKillIntentionBar(float ki, float maxKi, string skillName)
    {
        killIntentionBar.value = ki / maxKi;
        if (ki == maxKi)
        {
            kiTxt.text = skillName;
            pressF.SetActive(true);
        }
        else
        {
            kiTxt.text = Mathf.FloorToInt(ki) + " / " + Mathf.FloorToInt(maxKi);
            pressF.SetActive(false);
        }
        
    }
    public void UpdateMagazineBar(float magazing, float maxMagazing, bool reloading)
    {
        magazineBar.value = magazing / maxMagazing;
        if (reloading)
        {
            magazineTxt.text = "Reloading...";
        }
        else
        {
            if (magazing == 0)
            {
                magazineTxt.text = "Press R to Reload!";
            }
            else
            {
                magazineTxt.text = Mathf.FloorToInt(magazing) + " / " + Mathf.FloorToInt(maxMagazing);
            }
            
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}

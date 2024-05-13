using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{

    public GameObject InteractPrompt;
    public GameObject playerUI;
    public GameObject ShopUI;
    public Slider healthBar;
    public Slider killIntentionBar;
    public Slider magazineBar;
    public Slider bossHpBar;
    public TMP_Text hpTxt;
    public TMP_Text kiTxt;
    public GameObject pressF;
    public TMP_Text magazineTxt;

    public GameObject pauseUI;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject bossUI;

    public TMP_Text fpsUI;

    private int fps;
    private float fpsDisplayCD = 1.0f;
    private float fpsDisplayTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ShowPlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUI.activeInHierarchy && (Input.GetKeyDown("p")|| Input.GetKeyDown("escape")))
        {
            PauseGame();
            // show pause ui and cursor
        }
        else if(pauseUI.activeInHierarchy && (Input.GetKeyDown("p")|| Input.GetKeyDown("escape")))
        {
            UnPauseGame();
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
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowPlayerUI()
    {
        playerUI.SetActive(true);
        //winUI.SetActive(false);
        //loseUI.SetActive(false);
        pauseUI.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        //GameManager.instance.pause = true;
        pauseUI.SetActive(true);
        playerUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = false;
        GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = false;
        GameObject.FindWithTag("Player").GetComponentInChildren<ShotGun>().enabled = false;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        //GameManager.instance.pause = false;
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerUI.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = true;
        GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = true;
        GameObject.FindWithTag("Player").GetComponentInChildren<ShotGun>().enabled = true;
    }
    public void ShowWinUI()
    {
        playerUI.SetActive(false);
        bossUI.SetActive(false);
        winUI.SetActive(true);
    }
    public void ShowLoseUI() 
    {
        playerUI.SetActive(false);
        bossUI.SetActive(false);
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
    public void ShowBossUI()
    {
        bossUI.SetActive(true);
    }
    public void HideBossUI()
    {
        bossUI.SetActive(false);
    }
    public void UpdateBossHpBar(float hp, float hpMax)
    {
        bossHpBar.value = hp / hpMax;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}

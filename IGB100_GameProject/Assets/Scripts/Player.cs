using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 100;
    public float maxHealth;
    public float killIntention;
    public float maxKillIntention;
    public int ultimateSkillID = 1;
    private string ultimateSkillName = "Heal";
    public GameObject mainCamera;
    public GameObject[] weapons;

    //UI Elements
    public GameObject uiController;
    
    // Use this for initialization
    void Start () {
        uiController.GetComponent<UIController>().UpdateHpBar(health, maxHealth);
        uiController.GetComponent<UIController>().UpdateKillIntentionBar(killIntention, maxKillIntention, ultimateSkillName);
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.menu && !GameManager.instance.pause && Input.GetKey("f"))
        {
            UseUltimateSkill();
        }

    }

    public void TakeDamage(float dmg) {
        // increase kill intention when hurt
        killIntention += dmg * 100 / maxHealth;
        if (killIntention > maxKillIntention)
        {
            killIntention = maxKillIntention;
        }
        uiController.GetComponent<UIController>().UpdateKillIntentionBar(killIntention, maxKillIntention, ultimateSkillName);

        // lose hp when hurt , 0 to die
        health -= dmg;
        uiController.GetComponent<UIController>().UpdateHpBar(health, maxHealth);
        
        if (health <= 0) {
            GameManager.instance.GameOver();
            GameManager.instance.win = false;
            this.gameObject.SetActive(false);
        }
    }

    public void AddKillIntention(float k)
    {
        killIntention += k;
        if (killIntention > maxKillIntention) 
        { 
            killIntention = maxKillIntention;
        }
        uiController.GetComponent<UIController>().UpdateKillIntentionBar(killIntention, maxKillIntention, ultimateSkillName);
        
    }

    public void UseUltimateSkill()
    {
        if(killIntention == maxKillIntention)
        {
            switch (ultimateSkillID)
            {
                case 1:
                    killIntention = 0;
                    USkill_Heal();
                    break;
                case 2:
                    break;
                default:
                    Debug.Log("Wrong ultimateSkillID: " + ultimateSkillID);
                    break;

            }
        }
        else
        {
            // show some prompt / ui
        }
    }

    public void USkill_Heal()
    {
        health += maxHealth / 2;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        uiController.GetComponent<UIController>().UpdateHpBar(health, maxHealth);
        uiController.GetComponent<UIController>().UpdateKillIntentionBar(killIntention, maxKillIntention, ultimateSkillName);
    }

    public void ChangeSkill(int ID, string name)
    {
        ultimateSkillID = ID;
        ultimateSkillName = name;
    }
    
}

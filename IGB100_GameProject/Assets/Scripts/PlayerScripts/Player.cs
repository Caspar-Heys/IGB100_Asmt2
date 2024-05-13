using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Player : MonoBehaviour {

    public float health = 100;
    public float maxHealth;
    public float killIntention;
    public float maxKillIntention;
    public float ShotGunFireRate;
    public float GrimbrandFireRate;
    public int ultimateSkillID;
    public string ultimateSkillName;
    public GameObject mainCamera;
    public GameObject[] weapons;
    public GameObject EnemyBullet;
    public IConsumable[] Potions;

    //UI Elements
    public GameObject uiController;
    
    // Use this for initialization
    void Start () 
    {
        SwitchWeapon(1);
        if (ultimateSkillID == 1) { ultimateSkillName = "Heal"; }
        else if(ultimateSkillID == 2){ ultimateSkillName = "Adrenaline Surge"; }
        uiController.GetComponent<UIController>().UpdateHpBar(health, maxHealth);
        uiController.GetComponent<UIController>().UpdateKillIntentionBar(killIntention, maxKillIntention, ultimateSkillName);
        Potions[0] = gameObject.GetComponentInChildren<DeftFinger>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.menu && !GameManager.instance.pause)
        {
            if (Input.GetKeyDown("f"))
            {
                UseUltimateSkill();
            }
            if (Input.GetKeyDown("1"))
            {
                SwitchWeapon(1);
            }
            if (Input.GetKeyDown("2"))
            {
                SwitchWeapon(2);
            }
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
            GameManager.instance.win = false;
            GameManager.instance.GameOver();
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
    public void SwitchWeapon(int playerInput)
    {
        if(playerInput == 1) //Switches to Pistol
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            uiController.GetComponent<UIController>().UpdateMagazineBar(weapons[0].GetComponent<Grimbrand>().magazine, weapons[0].GetComponent<Grimbrand>().maxMagazine, weapons[0].GetComponent<Grimbrand>().reloading);
        }
        else if(playerInput == 2) //Switches to ShotGun
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            uiController.GetComponent<UIController>().UpdateMagazineBar(weapons[1].GetComponent<ShotGun>().magazine, weapons[1].GetComponent<ShotGun>().maxMagazine, weapons[1].GetComponent<ShotGun>().reloading);
        }
        // need to add another if statement for bazooka
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
                    killIntention = 0;
                    USkill_AdrenalineSurge();
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

    public void USkill_AdrenalineSurge()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<NavMeshAgent>().speed /= 2;
        }
        foreach(GameObject projectile in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            projectile.GetComponent<EnemyBulletRed>().moveSpeed /= 2;
        }
        EnemyBullet.GetComponent<EnemyBulletRed>().moveSpeed /= 2;
    }

    public void ChangeUltimateSkill(int ID, string name)
    {
        ultimateSkillID = ID;
        ultimateSkillName = name;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    
    public float healthMax = 100;
    private float health;
    public float hitDamage = 25;
    private float damageTimer;
    private float damageRate = 1.0f;

    private bool firing = false;

    public float killIntention = 10.0f;
    public int token = 1;
    public int score = 100;

    public bool isBoss = false;

    //Effects
    public GameObject deathEffect;

	// Use this for initialization
	void Start () {
        health = healthMax;
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    //Public method for taking damage and dying
    public void TakeDamage(float dmg) {
        health -= dmg;
        Debug.Log("hit");
        if (isBoss )
        {
            GameManager.instance.UpdateBossHpBar(health, healthMax);
            if (GetComponent<BossManagement>().GetBattleStage() == 1 && health < healthMax / 2)
            {
                GetComponent<BossManagement>().SetBattleStage(2);
            }
            
        }
        if (health <= 0) {
            if (isBoss)
            {
                GameManager.instance.win = true;
                GameManager.instance.GameOver();
                
            }
            Instantiate(deathEffect, transform.position, transform.rotation);
            GameManager.instance.token += token;
            GameManager.instance.score += score;
            GameManager.instance.player.GetComponent<Player>().AddKillIntention(killIntention);
            Destroy(this.gameObject);
        }
    }
    public void AddHp(float add)
    {
        health += add;
        if(health > healthMax)
        {
            health = healthMax;
        }
        if (isBoss)
        {
            GameManager.instance.UpdateBossHpBar(health, healthMax);
        }
    }
    private void OnTriggerStay(Collider otherObject) {

        if (otherObject.transform.tag == "Player" && Time.time > damageTimer) {
            otherObject.GetComponent<Player>().TakeDamage(hitDamage);
            damageTimer = Time.time + damageRate;
        }
    }

    public void SetFiring(bool f)
    {
        firing = f;
    }
    public bool GetFiring()
    {
        return firing;
    }
}

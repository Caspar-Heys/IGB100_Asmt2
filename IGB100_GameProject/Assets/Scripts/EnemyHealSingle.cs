using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealSingle : MonoBehaviour
{
    public float waitingTime = 1.0f;
    private float spawnTime = 0.0f;

    public float heal = 300.0f;
    public float healRate = 3.0f;
    private float healTimer = 0.0f;
    public float healRange = 10.0f;
    public int healRound = 10;
    private bool healing = false;
    public string targetName;
    private GameObject target;
    public GameObject healBullet;
    public GameObject effectHeal;
    public GameObject muzzle;


    public float healingAnimationCD = 1.0f;
    private float healingAnimationTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        try
        {
            target = GameObject.Find(targetName);
        }
        catch
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > waitingTime && healRound > 0)
        {
            HealSingle();
        }
    }

    private void HealSingle()
    {
        if (Time.time > healTimer)
        {
            healingAnimationTimer = Time.time;
            healing = true;
            GetComponent<Enemy>().SetFiring(true);
        
            transform.LookAt(target.transform.position);
            Instantiate(healBullet, muzzle.transform.position, muzzle.transform.rotation);
            //GameObject tempHeal = Instantiate(effectHeal, transform.position, transform.rotation);
            //tempHeal.GetComponent<EffectOnObject>().SetTarget(this.gameObject);
                
            healTimer = Time.time + healRate;
            healRound--;
        }
        if (healing && Time.time > healingAnimationTimer + healingAnimationCD)
        {
            GetComponent<Enemy>().SetFiring(false);
            healing = false;
        }
    }
}

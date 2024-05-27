using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealAura : MonoBehaviour
{
    public float waitingTime = 1.0f;
    private float spawnTime = 0.0f;

    public float heal = 300.0f;
    public float healRate = 3.0f;
    private float healTimer = 0.0f;
    public float healRange = 10.0f;
    public int healRound = 10;
    private bool healing = false;
    private GameObject[] targets;
    public GameObject effectHeal;
    
    public float healingAnimationCD = 1.0f;
    private float healingAnimationTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > waitingTime && healRound > 0)
        {
            HealAura();
        } 
    }

    private void HealAura()
    {
        if (Time.time > healTimer)
        {
            healingAnimationTimer = Time.time;
            healing = true;
            GetComponent<Enemy>().SetFiring(true);
            targets = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < healRange)
                {
                    //transform.LookAt(target.transform.position);
                    target.GetComponent<Enemy>().AddHp(heal);
                    GameObject tempHeal = Instantiate(effectHeal, target.transform.position, target.transform.rotation);
                    tempHeal.GetComponent<EffectOnObject>().SetTarget(target);
                } 
            }
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

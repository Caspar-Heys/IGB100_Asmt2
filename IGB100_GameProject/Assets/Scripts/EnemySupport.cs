using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySupport : MonoBehaviour
{
    public float waitingTime = 1.0f;
    private float spawnTime = 0.0f;

    public float supportRate = 0.5f;
    private float supportTimer = 0.0f;
    public float supportRange = 10.0f;
    
    private bool supporting = false;
    private GameObject[] targets;

    public float supportingAnimationCD = 1.0f;
    private float supportingAnimationTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > waitingTime)
        {
            Support();
        }
    }

    private void Support()
    {
        if (Time.time > supportTimer)
        {
            supportingAnimationTimer = Time.time;
            supporting = true;
            GetComponent<Enemy>().SetFiring(true);
            targets = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject target in targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) < supportRange)
                {
                    //transform.LookAt(target.transform.position);
                    target.GetComponent<Enemy>().SetSupport(1.0f);
                }
            }
            supportTimer = Time.time + supportRate;
        }
        if (supporting && Time.time > supportingAnimationTimer + supportingAnimationCD)
        {
            GetComponent<Enemy>().SetFiring(false);
            supporting = false;
        }
    }
   
}

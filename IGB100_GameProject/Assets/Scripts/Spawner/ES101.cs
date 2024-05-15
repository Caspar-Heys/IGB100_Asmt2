using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES101 : MonoBehaviour
{
    public GameObject enemy;
    public float waitingTime = 2.0f;
    public float spawnRate = 0.5f;
    private float spawnTimer = 0.0f;
    public int spawnNumber = 5;
    public float rngX = 0.0f;
    public float rngZ = 0.0f;
    public bool isBoss = false;

    // Start is called before the first frame update
    void Start()
    {
        waitingTime += GameManager.instance.startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time > waitingTime) && (spawnNumber > 0))
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (Time.time > spawnTimer)
        {
            Instantiate(enemy, transform.position + new Vector3(Random.Range(-rngX, rngX), 0, Random.Range(-rngZ, rngZ)), transform.rotation);
            spawnTimer = Time.time + spawnRate;
            spawnNumber--;
            if (isBoss)
            {
                GameManager.instance.BossFight(true);
            }
        }
    }
}

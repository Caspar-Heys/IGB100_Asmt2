using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;

    public float spawnRate = 3.0f;

    private float spawnTimer;

	// Update is called once per frame
	void Update () {
		
        if(Time.time > spawnTimer) {
            Instantiate(enemy, transform.position, transform.rotation);
            spawnTimer = Time.time + spawnRate;
        }
	}
}

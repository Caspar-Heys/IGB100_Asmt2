using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAround : MonoBehaviour
{
    public float waitingTime = 1.0f;
    private float spawnTime = 0.0f;

    public int fireRound = 1;
    public float fireRate = 1.0f;
    private float fireTime = 0.0f;

    // shoot around
    public int fireAround = 6;

    public GameObject enemybullet;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > waitingTime && fireRound > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > fireTime)
        {
            for (int j = 0; j < fireAround; j++)
            {
                Instantiate(enemybullet, transform.position, transform.rotation * Quaternion.Euler(360f / fireAround * j, 0f, 0f));
            }
            fireTime = Time.time + fireRate;
            fireRound--;
        }
    }

    public void addWaitingTime(float time)
    {
        waitingTime = time;
    }
}

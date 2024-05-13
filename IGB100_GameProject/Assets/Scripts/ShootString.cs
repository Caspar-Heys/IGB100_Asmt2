using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootString : MonoBehaviour
{
    public float waitingTime = 1.0f;
    private float spawnTime = 0.0f;

    public int fireRound = 10;
    public float fireRate = 3.0f;
    private float fireTimer = 0.0f;
    public float bulletSpeed = 10.0f;
    public float bulletSpeedPlus = 13.0f;
    
    // string of bullets
    public int fireString = 1;
    public float bulletSpeedDown = 1.0f;
    // public float stringInterval = 0.1f;
    private bool firing = false;
    public float firingAnimationCD = 1.0f;
    private float firingAnimationTimer;

    public GameObject enemybullet;
    public GameObject enemybulletPlus;
    private GameObject player;
    public GameObject muzzle;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        player = GameObject.FindWithTag("Player");
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
        if (Time.time > fireTimer)
        {
            firingAnimationTimer = Time.time;
            firing = true;
            GetComponent<Enemy>().SetFiring(true);
            transform.LookAt(player.transform.position);
            muzzle.transform.LookAt(player.transform.position);
            for (int i = 0; i < fireString; i++)
            {
                if (GetComponent<Enemy>().CheckIsSupported()) 
                {
                    GameObject enemyBulletPlus = Instantiate(enemybulletPlus, muzzle.transform.position, muzzle.transform.rotation);
                    enemyBulletPlus.GetComponent<EnemyBulletRed>().SetSpeed(bulletSpeedPlus - i * bulletSpeedDown);                }
                else
                {
                    GameObject enemyBullet = Instantiate(enemybullet, muzzle.transform.position, muzzle.transform.rotation);
                    enemyBullet.GetComponent<EnemyBulletRed>().SetSpeed(bulletSpeed - i * bulletSpeedDown);
                }    
                
            }
            fireTimer = Time.time + fireRate;
            fireRound--;
            
        }
        if (firing && Time.time > firingAnimationTimer + firingAnimationCD)
        {
            GetComponent<Enemy>().SetFiring(false);
            firing = false;
        }
        
    }

    public void addWaitingTime(float time)
    {
        waitingTime = time;
    }
    
}

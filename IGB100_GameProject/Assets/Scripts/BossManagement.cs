using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManagement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    private GameObject player;
    public GameObject muzzleL;
    public GameObject muzzleR;
    private GameObject muzzle;
    public GameObject bossBullet;
    private Quaternion rotation;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float distanceXZ = 0.0f;

    private int battleStage = 1;

    public bool firing = false;
    //public float firingAnimationDuration = 1.0f;
    //private float firingAnimationTimer;
    public int shootSpreadRoundMax = 3;
    private int shootSpreadRound;
    public float shootSpreadRate = 0.3f;
    private float shootSpreadTimer = 0.0f;
    public float shootSpreadAngle = 10.0f;
    public int shootSpreadNumber = 7;
    private int subNumber;
    public float bulletSpeed = 10.0f;
    private bool isLeftMuzzle = true;

    // for stage 2
    private bool locked = false;
    private Vector3 TargetPosition;
    public float speed = 5.0f;
    public float chargeSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        shootSpreadRound = shootSpreadRoundMax;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {  
        if (firing)
        {
            ShootSpreadly();
            if (shootSpreadRound == 0)
            {
                firing = false;
                GetComponent<Enemy>().SetFiring(false);
                locked = false;
                agent.speed = speed;
            }          
        }
        else
        {
            Movement();
        }  
    }

    private void Movement()
    {
        if (battleStage == 1)
        {
            // if no waypoint or reach the end point
            if (waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length)
                return;
            else if (GetComponent<Enemy>().GetFiring() == false)
            {
                // move towards current waypoint
                agent.destination = waypoints[currentWaypointIndex].position;
                distanceXZ = (transform.position.x - waypoints[currentWaypointIndex].position.x) * (transform.position.x - waypoints[currentWaypointIndex].position.x) + (transform.position.z - waypoints[currentWaypointIndex].position.z) * (transform.position.z - waypoints[currentWaypointIndex].position.z);
                // if close enough, change current waypoint to the next one
                if (distanceXZ < 0.2f)
                {
                    currentWaypointIndex++;
                    //firingAnimationTimer = Time.time;
                    firing = true;
                    shootSpreadRound = shootSpreadRoundMax;
                    GetComponent<Enemy>().SetFiring(true);
                }
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
        }
        else if (battleStage == 2)
        {
            if (!locked)
            {
                TargetPosition = player.transform.position;
                agent.destination = TargetPosition;
                locked = true;
                agent.speed = chargeSpeed;
            }
            else
            {
                distanceXZ = (transform.position.x - TargetPosition.x) * (transform.position.x - TargetPosition.x) + (transform.position.z - TargetPosition.z) * (transform.position.z - TargetPosition.z);
                // if close enough, 
                if (distanceXZ < 0.2f)
                {
                    firing = true;
                    shootSpreadRound = shootSpreadRoundMax;
                    GetComponent<Enemy>().SetFiring(true);
                }
            }
        }
        
    }
    public void ShootSpreadly()
    {
        transform.LookAt(player.transform.position);
        rotation = transform.rotation;
        if (Time.time > shootSpreadTimer)
        { 
            if (shootSpreadRound > 0)
            {
                if (isLeftMuzzle)
                {
                    muzzle = muzzleL;
                }
                else
                {
                    muzzle = muzzleR;
                }
                if (shootSpreadRound % 2 == 0)
                {
                    subNumber = shootSpreadNumber;
                }
                else
                {
                    subNumber = shootSpreadNumber + 1;
                }
                muzzle.transform.LookAt(player.transform.position);
                rotation = muzzle.transform.rotation;
                for (int j = 0; j < subNumber; j++)
                {
                    GameObject enemyBullet = Instantiate(bossBullet, muzzle.transform.position, rotation * Quaternion.Euler(0f, shootSpreadAngle * (-subNumber + 1 + j * 2), 0f));
                    enemyBullet.GetComponent<EnemyBulletRed>().SetSpeed(bulletSpeed);
                }
                shootSpreadRound--;
                isLeftMuzzle = !isLeftMuzzle;
            }
            shootSpreadTimer = Time.time + shootSpreadRate;
        }
    }
    public void SetBattleStage(int bs)
    {
        battleStage = bs;
        if (bs == 2)
        {
            bulletSpeed = bulletSpeed * 1.5f;
            shootSpreadRate = shootSpreadRate / 1.5f;
            shootSpreadRoundMax = shootSpreadRoundMax * 2;
        }
        
    }
    public int GetBattleStage()
    {
        return battleStage;
    }

}

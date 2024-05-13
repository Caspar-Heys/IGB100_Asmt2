using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManagementJump : MonoBehaviour
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
    private Vector3 targetPosition;
    public float speed = 5.0f;
    public float chargeSpeed = 8.0f;

    //for jump
    public float jumpHeight = 2f;
    public float jumpDuration = 1f;
    private Vector3 initialPosition;
    private float jumpStartTime;

    public GameObject lockingEffect;
    public GameObject boom;
    public float boomDamage = 25.0f;
    public float boomRange = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        shootSpreadRound = shootSpreadRoundMax;
        agent.speed = speed;
        //battleStage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentWaypointIndex: " + currentWaypointIndex + " firing: " + GetComponent<Enemy>().GetFiring() + " distanceXZ: " + distanceXZ);
        if (GetComponent<Enemy>().GetFiring())
        {
            
            ShootSpreadly();
            if (shootSpreadRound == 0)
            {
                //firing = false;
                GetComponent<Enemy>().SetFiring(false);
                locked = false;
                //agent.speed = speed;
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
            if (waypoints.Length == 0)
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
                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                    }
                    //firingAnimationTimer = Time.time;
                    //firing = true;
                    shootSpreadRound = shootSpreadRoundMax;
                    GetComponent<Enemy>().SetFiring(true);
                }
            }
        }
        else if (battleStage == 2)
        {
            
            if (!locked)
            {
                targetPosition = player.transform.position;
                initialPosition = transform.position;
                //agent.destination = TargetPosition;
                jumpStartTime = Time.time;
                locked = true;
                Instantiate(lockingEffect, new Vector3(player.transform.position.x,0.1f, player.transform.position.z), player.transform.rotation);
                //agent.speed = chargeSpeed;

            }
            else
            {
                JumpToTarget();
            }
            if (GetComponent<Enemy>().GetFiring() == false)
            {
                agent.destination = targetPosition;
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
            shootSpreadRound = 0;
            //agent = null;
            //firing = false;
            GetComponent<Enemy>().SetFiring(false);
        }
    }

    public int GetBattleStage()
    {
        return battleStage;
    }

    private void JumpToTarget()
    {
        float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;
        if (jumpProgress >= 1.0f)
        {
            transform.position = targetPosition;
            locked = false;
            //firing = true;
            shootSpreadRound = shootSpreadRoundMax;
            GetComponent<Enemy>().SetFiring(true);
            Instantiate(boom, transform.position, transform.rotation);
            if (Vector3.Distance(player.transform.position, transform.position) <= boomRange)
            {
                player.GetComponent<Player>().TakeDamage(boomDamage);
            }
        }
        else
        {
            float yOffset = Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, jumpProgress) + Vector3.up * yOffset;
        }
    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOnePoint : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    private Transform waypoint;
    //private float distanceXZ = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //GameObject prefab = Resources.Load(prefabName) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Vector3.Distance(transform.position, waypoint.position) < 0.2f)
        {
            return;
        }
        else if (GetComponent<Enemy>().GetFiring() == false)
        {
            agent.destination = waypoint.position;
        }
    }

    public void SetWayPoint(Transform w)
    {
        waypoint = w;
    }

}

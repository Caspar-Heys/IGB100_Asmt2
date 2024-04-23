using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        try
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }
    }
}

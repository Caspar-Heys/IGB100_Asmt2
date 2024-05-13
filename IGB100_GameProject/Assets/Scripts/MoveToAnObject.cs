using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToAnObject : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public string targetName;
    public float distance = 8.0f;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        try
        {
            target = GameObject.Find(targetName);
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
        if (Vector3.Distance(transform.position, target.transform.position) > distance)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    public void SetTarget(GameObject g)
    {
        target = g;
    }
}

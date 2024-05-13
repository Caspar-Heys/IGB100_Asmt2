using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdrenalineSurge : MonoBehaviour, ISkill
{
    public void UseSkill()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<NavMeshAgent>().speed /= 2;
        }
    }
}

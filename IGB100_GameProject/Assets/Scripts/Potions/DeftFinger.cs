using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeftFinger : MonoBehaviour, IConsumable
{
    public void UsePotion()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().ShotGunFireRate *= 0.9f;
        GameObject.FindWithTag("Player").GetComponent<Player>().GrimbrandFireRate *= 0.9f;
        Debug.Log("deftFinger used");
        Destroy(gameObject);
    }
}

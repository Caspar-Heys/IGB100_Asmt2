using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeftFinger : MonoBehaviour, IConsumable
{
    public void UsePotion()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerGun>().ShotGunfireRate *= 0.9f;
        GameObject.FindWithTag("Player").GetComponent<PlayerGun>().RiflefireRate *= 0.9f;
        GameObject.FindWithTag("Player").GetComponent<PlayerGun>().RPGfireRate *= 0.9f;
        Debug.Log("deftFinger used");
        Destroy(gameObject);
    }
}

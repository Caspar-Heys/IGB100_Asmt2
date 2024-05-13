using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable { public void UsePotion(); }
public class UsePotion : MonoBehaviour
{
    private void Update()
    {
        if(!GameManager.instance.menu && !GameManager.instance.pause)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject.FindWithTag("ActiveSkill1").GetComponent<DeftFinger>().UsePotion();
            }
        }
    }
}

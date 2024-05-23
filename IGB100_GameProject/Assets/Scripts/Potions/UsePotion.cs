using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable { public void UsePotion(); }
public class UsePotion : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    private void Update()
    {
        if(playerUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject.FindWithTag("ActiveSkill1").GetComponent<DeftFinger>().UsePotion();
            }
        }
    }
}

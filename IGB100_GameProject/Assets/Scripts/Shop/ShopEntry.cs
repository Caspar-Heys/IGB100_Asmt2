using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour, IInteractable
{
    public GameObject ShopUI;
    public void Interact()
    {
        ShopUI.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = false;

    }
    private void Start()
    {
        ShopUI.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(ShopUI.activeInHierarchy)
                ExitShop();
        }
    }
    
    private void ExitShop()
    {
        ShopUI.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

}

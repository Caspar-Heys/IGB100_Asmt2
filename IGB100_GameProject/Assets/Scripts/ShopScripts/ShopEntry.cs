using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour, IInteractable
{
    public GameObject ShopUI;
    public GameObject playerUI;
    public void Interact()
    {
        ShopUI.SetActive(true);
        playerUI.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = false;
        GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = false;

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
        playerUI.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = true;
        GameObject.FindWithTag("Player").GetComponentInChildren<Grimbrand>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

}

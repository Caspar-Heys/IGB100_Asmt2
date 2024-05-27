using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopEntry : MonoBehaviour, IInteractable
{
    public GameObject ShopUI;
    public GameObject playerUI;
    public TextMeshProUGUI Tokenstxt;
    public void Interact()
    {
        ShopUI.SetActive(true);
        playerUI.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = false;
        GameObject.FindWithTag("Player").GetComponentInChildren<PlayerGun>().enabled = false;
        Tokenstxt.text = "Tokens: " + GameObject.FindWithTag("Player").GetComponent<Player>().tokens;

    }
    private void Start()
    {
        ShopUI.SetActive(false);
    }

    
    public void ExitShop()
    {
        ShopUI.SetActive(false);
        playerUI.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<Interaction>().enabled = true;
        GameObject.FindWithTag("Player").GetComponentInChildren<PlayerGun>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

}

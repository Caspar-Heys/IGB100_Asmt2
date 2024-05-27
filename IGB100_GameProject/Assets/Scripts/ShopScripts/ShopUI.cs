using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopItemTitle;//Title txt on item template
    public GameObject ShopItemDescription;//Description txt on item template
    public GameObject ShopItemPrice;//Price txt on item template
    public GameObject BuyBtn;//Purchase button on item template
    public GameObject CannotAffordtxt;
    public TextMeshProUGUI Tokenstxt;

    private float ButtonCDtimer;
    private float ButtonCDrate = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        string name = gameObject.GetComponent<ItemAttributes>().name;
        int price = gameObject.GetComponent<ItemAttributes>().price;
        ShopItemTitle.GetComponent<TextMeshProUGUI>().text = name;
        ShopItemPrice.GetComponent<TextMeshProUGUI>().text = price + " tokens";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void BuyItem()
    {
        if (Time.time > ButtonCDtimer)
        {
            int itemPrice = gameObject.GetComponent<ItemAttributes>().price;
            if (GameObject.FindWithTag("Player").GetComponent<Player>().tokens >= itemPrice)
            {
                //Add item to player inventory, subtract cost from tokens, play purchase audio
                Destroy(gameObject);
                GameObject.FindWithTag("Player").GetComponent<Player>().tokens -= itemPrice;
                Tokenstxt.text = "Tokens: " + GameObject.FindWithTag("Player").GetComponent<Player>().tokens;
            }
            else
            {
                var colors = BuyBtn.GetComponent<Button>().colors;
                colors.pressedColor = Color.red;
                BuyBtn.GetComponent<Button>().colors = colors;
                Instantiate(CannotAffordtxt, GameObject.FindWithTag("UIController").GetComponent<UIController>().ShopUI.transform);
            }
            ButtonCDtimer = Time.time + ButtonCDrate;
        }
    }
}

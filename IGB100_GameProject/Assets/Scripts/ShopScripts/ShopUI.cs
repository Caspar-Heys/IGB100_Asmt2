using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject ShopItemTitle;//Title txt on item template
    public GameObject ShopItemDescription;//Description txt on item template
    public GameObject ShopItemPrice;//Price txt on item template
    public GameObject BuyBtn;//Purchase button on item template
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
        int itemPrice = gameObject.GetComponent<ItemAttributes>().price;
        if(GameManager.GetComponent<GameManager>().token >= itemPrice)
        {

            //Add item to player inventory, subtract cost from tokens, play purchase audio
            Destroy(gameObject);
            GameManager.GetComponent<GameManager>().token -= itemPrice;
        }

        else
        {
            var colors = BuyBtn.GetComponent<Button>().colors;
            colors.selectedColor = Color.red;
            BuyBtn.GetComponent<Button>().colors = colors;


        }


    }
}

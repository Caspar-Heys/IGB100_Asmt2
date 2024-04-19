using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEntry : MonoBehaviour
{
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("e"))
            Debug.Log("balls and cock");
    }

    private void OnTriggerStay(Collider other)
    {
 
        if(other.gameObject.tag == "Shop")
        {
            inRange = true;
        }
    }
}

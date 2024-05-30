using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    private int token = 2;
    public GameObject tokenSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().tokens += token;
            GameManager.instance.uiController.GetComponent<UIController>().UpdateTokens(other.GetComponent<Player>().tokens);
            Instantiate(tokenSound, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void SetTokenValue(int t)
    {
        token = t;
    }
}

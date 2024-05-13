using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill { public void UseSkill(); }
public class UseSkill : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.menu && !GameManager.instance.pause)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (GameObject.FindWithTag("Player").GetComponent<Player>().killIntention >= 75)
                {
                    GameObject.FindWithTag("ActiveSkill1").GetComponent<HealSkill>().UseSkill();
                    GameObject.FindWithTag("Player").GetComponent<Player>().killIntention -= 75;
                    GameObject.FindWithTag("UIController").GetComponent<UIController>().UpdateKillIntentionBar(GameObject.FindWithTag("Player").GetComponent<Player>().killIntention, GameObject.FindWithTag("Player").GetComponent<Player>().maxKillIntention, GameObject.FindWithTag("Player").GetComponent<Player>().ultimateSkillName);
                }
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (GameObject.FindWithTag("Player").GetComponent<Player>().killIntention >= 100)
                {
                    GameObject.FindWithTag("ActiveSkill2").GetComponent<AdrenalineSurge>().UseSkill();
                    GameObject.FindWithTag("Player").GetComponent<Player>().killIntention -= 100;
                    GameObject.FindWithTag("UIController").GetComponent<UIController>().UpdateKillIntentionBar(GameObject.FindWithTag("Player").GetComponent<Player>().killIntention, GameObject.FindWithTag("Player").GetComponent<Player>().maxKillIntention, GameObject.FindWithTag("Player").GetComponent<Player>().ultimateSkillName);
                }
            }
        }
    }
}

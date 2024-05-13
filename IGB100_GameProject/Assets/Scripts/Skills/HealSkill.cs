using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : MonoBehaviour, ISkill
{
    private float healthGain;
    [SerializeField] private float SkillCD;
    // Start is called before the first frame update

    public void UseSkill()
    {
        float health = GameObject.FindWithTag("Player").GetComponent<Player>().health;
        float maxhealth = GameObject.FindWithTag("Player").GetComponent<Player>().maxHealth;
        healthGain = maxhealth * 0.3f;
        if(maxhealth < healthGain + health)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().health = maxhealth;
        }
        else
            GameObject.FindWithTag("Player").GetComponent<Player>().health += healthGain;
        GameObject.FindWithTag("UIController").GetComponent<UIController>().UpdateHpBar(GameObject.FindWithTag("Player").GetComponent<Player>().health, GameObject.FindWithTag("Player").GetComponent<Player>().maxHealth);
    }
}

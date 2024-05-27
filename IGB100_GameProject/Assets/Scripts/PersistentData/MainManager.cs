using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;    // Start is called before the first frame update

    public float currenthealth;
    public float maxhealth;
    public float killintention;
    public string CurrentSkill;
    public string ItemSlot1;
    public string ItemSlot2;
    public string PassiveItem;
    public int tokens;
    public int Level;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        currenthealth = 100;
        maxhealth = 100;
        killintention = 0;
        tokens = 20;

    }
}

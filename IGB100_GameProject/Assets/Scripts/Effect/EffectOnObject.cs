using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnObject : MonoBehaviour
{
    public float lifeTime = 2.0f;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.position = target.transform.position;
        }
        
    }

    public void SetTarget(GameObject g)
    {
        target = g;
    }
}

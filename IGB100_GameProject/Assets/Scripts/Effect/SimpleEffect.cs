using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEffect : MonoBehaviour
{
    public float lifeTime = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaProjectile : MonoBehaviour
{
    public float movespeed;
    public float rotatespeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.position += transform.forward * movespeed * Time.deltaTime;
    }


    public void OnCollisionEnter(Collision collision)
    {
        GameObject.FindWithTag("Player").GetComponentInChildren<PlayerGun>().CheckForDestructibles(this.transform);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRed : MonoBehaviour
{
    public float lifeTime = 10.0f;
    private float moveSpeed = 5.0f;
    public float damage = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }

    //OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().TakeDamage(damage);

            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}

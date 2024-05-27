using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletGreen : MonoBehaviour
{
    public float lifeTime = 10.0f;
    public float moveSpeed = 5.0f;
    public float heal = 300.0f;
    public GameObject effectHeal;
    public string targetName;
    private GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
        try
        {
            target = GameObject.Find(targetName);
        }
        catch
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.LookAt(target.transform.position + new Vector3(0, 1, 0));
        transform.position += moveSpeed * Time.deltaTime * transform.forward;
    }

    //OnTriggerEnter
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Player>().USkill_Heal(heal/10);
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Terrain")
        {
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponent<Enemy>().AddHp(heal);
            GameObject tempHeal = Instantiate(effectHeal, other.transform.position, other.transform.rotation);
            tempHeal.GetComponent<EffectOnObject>().SetTarget(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}

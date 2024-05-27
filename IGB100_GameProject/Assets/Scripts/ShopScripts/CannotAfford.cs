using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotAfford : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 95 * Time.deltaTime * new Vector3(0, -1, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public Rigidbody rb;
    float timeToDestroy = 10f;
    float time;
    void Update()
    {
        //transform.Translate((transform.forward * speed * Time.deltaTime));
        //rb.AddForce(transform.forward * speed);
        time += Time.deltaTime;
        if (time > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

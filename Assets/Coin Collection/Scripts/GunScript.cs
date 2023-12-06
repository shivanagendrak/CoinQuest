using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform target;
    public Transform firepoint;
    public Bullet bullet;
    float fireTimeIntervel = 0.75f;
    float time = 0f;
    [SerializeField] Enemy enemy;
    public AudioSource fireSound;

    // Start is called before the first frame update
    void OnEnable()
    {
        target = GameManager.Instance.playerController.transform;
        if(enemy==null)
        {
           enemy= GetComponentInParent<Enemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;

        transform.LookAt(target);
        float dist = Vector3.Distance(target.position, transform.position);
        if(enemy!=null && dist < enemy.followTargetDist)
        {
            time += Time.deltaTime;
            if (time >= fireTimeIntervel)
            {
                time = 0;
                Fire();
            }
        }
       
    }

    public void Fire()
    {
        Bullet tempBullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
        tempBullet.rb.AddForce(transform.forward * 500);
        fireSound.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
     public float health;
    public float maxHealth;
    // public Slider slider;

    private void OnEnable()
    {
        health = maxHealth;
    }
    public void SetHealth()
    {
        GameManager.Instance.slider.value = health;
        GameManager.Instance.healthText.text = health.ToString();
        //SoundManager.Instance.healthSound.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Pick Up")
        //{
        //    GameManager.Instance.CollectCoin();
        //    other.gameObject.SetActive(false);
        //}
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet Triger");
            health -= 2;
            SetHealth();

            if (health <= 0)
            {
                GameManager.Instance.GameOver(false);

            }
            Destroy(other.transform.parent.gameObject);
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("Enemy");
    //        health -= 20;
    //        SetHealth();
    //        //GameManager.Instance.slider.value = health;
    //        if (health <= 0)
    //        {
    //            GameManager.Instance.GameOver(false);
    //        }
    //    }
    //}
}

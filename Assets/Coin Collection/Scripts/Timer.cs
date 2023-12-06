using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Diagnostics;

public class Timer : MonoBehaviour
{
    //public TextMeshProUGUI timerText;
   public Image timerImage;
    public float timer;

    float currentTimer;

    bool timerRunning;
    private void OnEnable()
    {
        currentTimer = timer;
        
        timerRunning = true;
    }

    float minutes;
    float seconds;
    public void Update()
    {
        if (timerRunning && this.gameObject.activeSelf)
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
                timerImage.fillAmount = currentTimer / timer;
                 //minutes = Mathf.FloorToInt(currentTimer / 60);
                 //seconds = Mathf.FloorToInt(currentTimer % 60);
                 //timerText.text = minutes.ToString("00")+":"+seconds.ToString("00");
            }
            else
            {
                timerRunning = false;
                GameManager.Instance.GameOver(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {

    }
}

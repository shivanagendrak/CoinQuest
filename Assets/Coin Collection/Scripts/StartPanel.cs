using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public GameObject playButton;
    public GameObject subPanel;
    public GameObject timerGameobject;
    public GameObject enemyGameobject;
    private void OnEnable()
    {
        subPanel.SetActive(false);
        playButton.SetActive(true);
    }
    public void OnTimeBaseGame(bool timer)
    {
        timerGameobject.SetActive(timer);
        GameManager.Instance.slider.gameObject.SetActive(!timer);
        enemyGameobject.SetActive(!timer);
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
     //   OnPlayButtonClick();
    }
    public void OnPlayButtonClick()
    {
        subPanel.SetActive(true);
        playButton.SetActive(false);


    }
}

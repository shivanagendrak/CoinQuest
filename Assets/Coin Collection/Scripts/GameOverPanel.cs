using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public TextMeshProUGUI winText;

    public void OnHomeButtonCLick()
    {
        GameManager.Instance.startPanel.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnRestartButtonClick()
    {
        GameManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
}

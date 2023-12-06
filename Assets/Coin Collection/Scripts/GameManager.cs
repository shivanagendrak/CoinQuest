using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> coin;
    public List<Enemy> enemyList;

    public PlayerMovement playerController;
    public GameObject playertarget;

    public Transform startTransform;

    public int coinCollectCount;

    [Header("UI")]
    public StartPanel startPanel;
    public GameOverPanel gameOverPanel;
    public TextMeshProUGUI coinText;
    public Slider slider;
   public Text healthText;
    //public Image Bookslider;
    //public GameObject healthBar;

    private void Awake()
    {
        Instance = this;
       // StartGame();
    }

    public void StartGame()
    {
        playerController.ResetCoin();
        playerController.transform.position = startTransform.position;
        playerController.transform.eulerAngles = startTransform.eulerAngles;
        //healthBar.SetActive(true);
        slider.value = 100;
       // Bookslider.fillAmount = 0;
        //playerController.GetComponent<PlayerScript>().maxHealth = 100;
        //playerController.GetComponent<PlayerScript>().health = 100;
        healthText.text = 100.ToString();
        coinCollectCount = 0;
        coinText.transform.gameObject.SetActive(true);
        coinText.text = coinCollectCount + " / " + coin.Count;
        playerController.enabled = true;
        StartCoroutine(WaitAndStartCoinAndEnemy());
    }

    public IEnumerator WaitAndStartCoinAndEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < coin.Count; i++)
        {
            coin[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].gameObject.SetActive(true);
        }
    }

    public void CollectCoin()
    {
        coinCollectCount++;
        //Bookslider.fillAmount=coinCollectCount/coin.Count; 
        //coinText.text = coinCollectCount + " / " + coin.Count;
        //SoundManager.Instance.coinCollectSound.Play();
        if (coinCollectCount == coin.Count)
        {
            GameManager.Instance.startPanel.timerGameobject.SetActive(false);
            GameManager.Instance.startPanel.enemyGameobject.SetActive(false);
            GameOver(true);
        }
    }
    public void GameOver(bool isWin)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].gameObject.SetActive(false);
            enemyList[i].transform.position = enemyList[i].startPosition;
            enemyList[i].isFollowPlayer = false;
            enemyList[i].isRndom = true;
        }
        for (int i = 0; i < coin.Count; i++)
        {
            coin[i].gameObject.SetActive(false);
        }

        if (isWin)
        {
            //SoundManager.Instance.winSound.Play();
            gameOverPanel.winText.text = "Congratulation Win The Game";
        }
        else
        {
            //SoundManager.Instance.loseSound.Play();
            gameOverPanel.winText.text = "Sorry please try again";
        }
        //personController._animator.SetFloat(personController._animIDSpeed, 0);
      GameManager.Instance.slider.gameObject.SetActive(false);
        playerController.enabled = false;
        coinText.transform.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(true);
    }
}

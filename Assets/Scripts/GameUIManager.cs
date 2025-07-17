using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.SocialPlatforms.Impl;
public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }
    // Start is called before the first frame update
    public TextMeshProUGUI collectText,healthText;
    public int collectedObj;
    public int health;
    public GameObject loosePanel, winPanel,gameUIPanel;
    public int roundTime;

    public event Action GameRestart;
    public event Action NextRound;
    public event Action GameOverEvent;
   
    private void Awake()
    {

        Debug.Log(PlayerNameManager.playerName);
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Zaten varsa bunu yok et
            return;
        }

        Instance = this;
       //  DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde kalsýn istiyorsan aç
    }

    private void OnEnable()
    {
        // Event'e abone ol
        if (RoundTimer.Instance != null)
        {
            Debug.Log("oyun yeniden baþladý");
            RoundTimer.Instance.OnTimerEnd += GameOver;
        }
    }
    

    private void OnDisable()
    {
        // Event'ten ayrýl (önemli!)
        if (RoundTimer.Instance != null)
        {
            RoundTimer.Instance.OnTimerEnd -= GameOver;
        }
    }
    
    void Start()
    {
        collectedObj = 0;
        health = 3;
        winPanel.SetActive(false);
        loosePanel.SetActive(false);
        Time.timeScale = 1.0f;
        collectText.text = collectedObj + " /5 " ;
        healthText.text = "Attempt= " + health;

        GameRestart?.Invoke();
        RoundTimer.Instance.StartTimer(roundTime);
        Invoke(nameof(HookToEvent), 0.1f);
      

    }
    
    void Update()
    {
        
    }
    public void IncreaseCollectbleObj()
    {
        if(collectedObj <4)
        {
            collectedObj++;
            collectText.text = "5 / " + collectedObj;
            AudioController.Instance.effect.Stop(); // Eski efekt sesi varsa durdur
            AudioController.Instance.PlayEffect(AudioController.Instance.win);
            
        }
        else
        {
            GameOverEvent?.Invoke();
            winPanel.SetActive(true);
            gameUIPanel.SetActive(false);    
            Debug.Log("<color=green>NEXT ROUND</color>");
          //  ScoreManager.SaveScore(PlayerNameManager.playerName, 1 / RoundTimer.Instance.remainingTime * 1000);
          //  HighScoreUI.Instance.HighScore();

        }
        
    }
    public void DecreaseCollectbleObj()
    {
        if(health>1)
        {
            health--;
            healthText.text = "Attempt= " + health;
        }
        else
        {
            Invoke("GameOver",1f);
         //  GameOver();
        }
       
    }
    public void GameOver()
    {

    
        GameOverEvent?.Invoke();
        RoundTimer.Instance.PauseTimer();
        loosePanel.SetActive(true);
        gameUIPanel.SetActive(false);
        Debug.Log("<color=red>GAME OVER</color>");
        
        AudioController.Instance.effect.Stop(); // Eski efekt sesi varsa durdur
        
       AudioController.Instance.PlayEffect(AudioController.Instance.loose);
    }

   public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void HookToEvent()
    {
        RoundTimer.Instance.OnTimerEnd -= GameOver;
        RoundTimer.Instance.OnTimerEnd += GameOver;
    }

    public void ToggleScene()
    {
       

        /*
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex == 0 ? 1 : 0;
        SceneManager.LoadScene(nextSceneIndex);
        */
    }
}

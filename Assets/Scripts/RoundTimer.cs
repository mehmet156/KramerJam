using TMPro;
using UnityEngine;
using System;

public class RoundTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float remainingTime;
    private float timeScale = 1f;
    private bool isRunning = false;

    public event Action OnTimerEnd;
    
    public static RoundTimer Instance { get; private set; }
    


    private void Awake()
    {
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Zaten varsa bunu yok et
            return;
        }

        Instance = this;
       // DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde kalsýn istiyorsan aç
    }
    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime * timeScale;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isRunning = false;
            OnTimerEnd?.Invoke();
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes}:{seconds:00}";
    }

    // Timer'ý baþlat
    public void StartTimer(float timeInSeconds)
    {
        remainingTime = timeInSeconds;
        isRunning = true;
        UpdateTimerDisplay();
    }

    //  Timer'ý durdur
    public void PauseTimer()
    {
        isRunning = false;
    }

    //  Timer'ý devam ettir
    public void ResumeTimer()
    {
        isRunning = true;
    }

    //  Timer'ý sýfýrla ve durdur
    public void ResetTimer()
    {
        remainingTime = 0;
        isRunning = false;
        UpdateTimerDisplay();
    }

    //Süreyi artýr (saniye cinsinden)
    public void AddTime(float seconds)
    {
        remainingTime += seconds;
    }

    // Süreyi azalt (saniye cinsinden)
    public void SubtractTime(float seconds)
    {
        remainingTime = Mathf.Max(0, remainingTime - seconds);
    }

    // Hýz çarpaný ayarla (1 = normal, 2 = hýzlý, 0.5 = yavaþ)
    public void SetTimeScale(float scale)
    {
        timeScale = Mathf.Max(0f, scale);
    }

    // Geriye kalan süreyi al
    public float GetRemainingTime()
    {
        return remainingTime;
    }

    //  Timer çalýþýyor mu?
    public bool IsRunning()
    {
        return isRunning;
    }

    
}

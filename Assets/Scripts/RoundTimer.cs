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
        // Singleton kontrol�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Zaten varsa bunu yok et
            return;
        }

        Instance = this;
       // DontDestroyOnLoad(gameObject); // Sahne ge�i�lerinde kals�n istiyorsan a�
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

    // Timer'� ba�lat
    public void StartTimer(float timeInSeconds)
    {
        remainingTime = timeInSeconds;
        isRunning = true;
        UpdateTimerDisplay();
    }

    //  Timer'� durdur
    public void PauseTimer()
    {
        isRunning = false;
    }

    //  Timer'� devam ettir
    public void ResumeTimer()
    {
        isRunning = true;
    }

    //  Timer'� s�f�rla ve durdur
    public void ResetTimer()
    {
        remainingTime = 0;
        isRunning = false;
        UpdateTimerDisplay();
    }

    //S�reyi art�r (saniye cinsinden)
    public void AddTime(float seconds)
    {
        remainingTime += seconds;
    }

    // S�reyi azalt (saniye cinsinden)
    public void SubtractTime(float seconds)
    {
        remainingTime = Mathf.Max(0, remainingTime - seconds);
    }

    // H�z �arpan� ayarla (1 = normal, 2 = h�zl�, 0.5 = yava�)
    public void SetTimeScale(float scale)
    {
        timeScale = Mathf.Max(0f, scale);
    }

    // Geriye kalan s�reyi al
    public float GetRemainingTime()
    {
        return remainingTime;
    }

    //  Timer �al���yor mu?
    public bool IsRunning()
    {
        return isRunning;
    }

    
}

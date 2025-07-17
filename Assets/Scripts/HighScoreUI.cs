using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    public Transform container;
    public GameObject entryPrefab;

    public static HighScoreUI Instance { get; private set; }
    private void Awake()
    {

       
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Zaten varsa bunu yok et
            return;
        }

        Instance = this;
      
    }

    void Start()
    {
        int count = PlayerPrefs.GetInt("ScoreCount", 0);
        List<(string, int)> scores = new();

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "Unknown");
            int score = PlayerPrefs.GetInt($"PlayerScore_{i}", 0);
            scores.Add((name, score));
        }

        // Skorlarý yüksekten düþüðe sýrala
        scores.Sort((a, b) => b.Item2.CompareTo(a.Item2));

        // UI'ya yerleþtir
        foreach (var (name, score) in scores)
        {
            GameObject entry = Instantiate(entryPrefab, container);
            entry.GetComponent<TMP_Text>().text = $"{name} - {score}";
        }
    }
    public void HighScore()
    {
        Debug.Log("funk çaðýrýlýyor  mu?");
        int count = PlayerPrefs.GetInt("ScoreCount", 0);
        List<(string, int)> scores = new();

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString($"PlayerName_{i}", "Unknown");
            int score = PlayerPrefs.GetInt($"PlayerScore_{i}", 0);
            scores.Add((name, score));
        }

        // Skorlarý yüksekten düþüðe sýrala
        scores.Sort((a, b) => b.Item2.CompareTo(a.Item2));

        // UI'ya yerleþtir
        foreach (var (name, score) in scores)
        {
            Debug.Log("ýnstantiate oluyor mu?");
            GameObject entry = Instantiate(entryPrefab, container);
            entry.GetComponent<TMP_Text>().text = $"{name} - {score}";
        }
    }
}

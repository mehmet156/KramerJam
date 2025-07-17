using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    public static void SaveScore(string name, float score)
    {
        float index = PlayerPrefs.GetFloat("ScoreCount", 0);
        PlayerPrefs.SetString($"PlayerName_{index}", name);
        PlayerPrefs.SetFloat($"PlayerScore_{index}", score);
        PlayerPrefs.SetFloat("ScoreCount", index + 1);
        PlayerPrefs.Save();
    }
    
}

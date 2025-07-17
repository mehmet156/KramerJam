using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField nameInput;
    public static string playerName;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerName = "No Name";
    }
    public void SaveName()
    {
        playerName = nameInput.text;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject howToPlayPanel, creditsPanel, mainPanel,introPage;
    
    public Sprite musicOn, musicOff;
    
    public Button musicButton;

    bool musicPlaying = true;  
    void Start()
    {
        
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
        introPage.SetActive(false);

        
        musicButton.image.sprite = musicOn;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IntroPage()
    {
        introPage.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void MainMenuActive()
    {
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void CreditsActive()
    {
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void HowToPlayActive()
    {
        howToPlayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        mainPanel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ToggleMusic()
    {
        if(musicPlaying)
        {
            musicPlaying = false;
            musicButton.image.sprite = musicOff;
        }
        else
        {
            musicPlaying = true;
            musicButton.image.sprite = musicOn;
        }
    }
}

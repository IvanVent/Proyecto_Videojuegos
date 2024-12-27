using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject optionsUI;
    private bool isPaused = false;
    private bool music=true;
    private bool vivo=true;
    public AudioClip sfx1,sfx2;
    public AudioSource src;
    private Player playera;
    public VolumeControl volumeControl;
    private void Start()
    {
        src.clip=sfx1;
        playera = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update(){
        if(vivo){
            if(music){
                StartCoroutine(backmusic());
            }
        }
        else{
            if(music){
                StartCoroutine(deadmusic());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                Continue();
            }
        }
        
    }
    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        GameObject.Find("PauseButton").GetComponent<Button>().interactable = false;
    }
    private IEnumerator deadmusic(){
        music=false;
        src.Play();
        yield return new WaitForSeconds(6);
        music=true;
    }
    private IEnumerator backmusic(){
        float startTime=Time.unscaledTime;
        music=false;
        src.Play();
        while(Time.unscaledTime-startTime<12){
            yield return null;
        }
        music=true;
    }
    public void GameOver()
    {
        music=false;
        vivo=false;
        src.Stop();
        src.clip=sfx2;
        gameOverUI.SetActive(true);
        music=true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Continue()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameObject.Find("PauseButton").GetComponent<Button>().interactable = true;

    }
    public void Options(){
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
    }
    public void Volver(){
        optionsUI.SetActive(false);
        pauseUI.SetActive(true);
    }
    public void addVolume(){
        volumeControl.AddVolume();
    }
    public void lessVolume(){
        volumeControl.LessVolume();
    }
    public void changeautorecolect(){
        playera.autorecolect=!playera.autorecolect;
    }
    
    
    
    
    
    
}

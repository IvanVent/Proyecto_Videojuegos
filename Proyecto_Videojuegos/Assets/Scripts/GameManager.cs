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
    private bool isPaused = false;
    private bool music=true;
    private bool vivo=true;
    public AudioClip sfx1,sfx2;
    public AudioSource src;

    private void Start()
    {
        src.clip=sfx1;
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
        music=false;
        src.Play();
        yield return new WaitForSeconds(12);
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
    
    
    
    
    
    
}

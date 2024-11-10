using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    //public ControllerRandom control;
    private bool music=true;
    private bool vivo=true;
    public AudioClip sfx1,sfx2;
    public AudioSource src;
    

    private void Start()
    {
        src.clip=sfx1;
        //control = gameObject.GetComponent<ControllerRandom>();
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
    
    
    
    
    
    
}

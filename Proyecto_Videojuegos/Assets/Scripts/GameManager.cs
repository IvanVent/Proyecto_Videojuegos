using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject optionsUI;
    public GameObject controlsUI;
    private bool isPaused = false;
    private bool music=true;
    private bool alive=true;
    public AudioClip sfx1,sfx2;
    public AudioSource src;
    private Player playera;
    public VolumeControl volumeControl;
    public Text optionText;
    private bool options=false;
    private GameObject []objetos;
    private void Start()
    {
        src.clip=sfx1;
        objetos=SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
        foreach(GameObject obj in objetos){
            if(obj.name=="Player"){
                playera = obj.GetComponent<Player>();
                break;
            }
        }
        optionText.text="Auto Pick Up: OFF";
    }
    void Update(){
        if(SceneManager.GetActiveScene()!=SceneManager.GetSceneByBuildIndex(1)){
        if(alive){
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
                if(options){
                    Volver();
                }
                
                Continue();
                
                
            }
        }
        }
        
    }
    
    // ---------------SONIDOS------------------
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
    // -----------------------------------------
    
    // Pantalla de pausa al pulsar el botón de pausa
    public void PauseGame()
    {
        if (alive)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            GameObject.Find("PauseButton").GetComponent<Button>().interactable = false;
        }
    }
    // Pantalla de Game Over
    public void GameOver()
    {
        music=false;
        alive=false;
        src.Stop();
        src.clip=sfx2;
        gameOverUI.SetActive(true);
        music=true;
    }
    
    // Acción del botón de Restart en la pantalla de Game Over
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name,LoadSceneMode.Single);
    }

    // Acción del botón de Salir
    public void MainMenu()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    // Acción del botón de Continuar en la pantalla de pausa
    public void Continue()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameObject.Find("PauseButton").GetComponent<Button>().interactable = true;

    }
    // Acción del botón de Opciones en la pantalla de pausa
    public void Options(){
        options=true;
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void Controls()
    {
        controlsUI.SetActive(true);
        pauseUI.SetActive(false);
    }
    // Acción del botón de Volver en la pantalla de pausa
    public void Volver(){
        if(SceneManager.GetSceneByBuildIndex(1).isLoaded){
            GameObject[] objetos= SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();
            GameObject[] otros=SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
            optionsUI.SetActive(false);
            foreach(GameObject obj in otros){
                if(obj.name=="EventSystem"){
                    obj.SetActive(false);
                }
            }
            foreach(GameObject obj in objetos){
                if(obj.name=="EventSystem"){
                    obj.SetActive(true);
                }
            }

        }
        else{
            options=false;
            optionsUI.SetActive(false);
            pauseUI.SetActive(true);
        }
       
    }
    
    // Acciones de los botones de ajuste de opciones
    public void addVolume(){
        volumeControl.AddVolume();
    }
    public void lessVolume(){
        volumeControl.LessVolume();
    }
    public void addSfx(){
        print("eh");
        volumeControl.AddSfx();
    }
    public void lessSfx(){
        volumeControl.LessSfx();
    }
    public void toogleAutoPickUp(){

        playera.autorecolect=!playera.autorecolect;
        if(playera.autorecolect){
            optionText.text="Auto Pick Up: ON";
        }
        else{
            optionText.text="Auto Pick Up: OFF";
        }
    }
    
    
    
    
    
    
}

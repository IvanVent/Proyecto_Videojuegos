using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject optionsUI;
    public GameObject controlsUI;
    public GameObject deadPanel;
    public GameObject pauseButton;
    
    
    private bool isPaused = false;
    private bool music=true;
    private bool alive=true;
    private bool isControlsScreen=false;
    private bool isEnding = false;
    private bool isIntro = true;
    
    public AudioClip sfx1,sfx2;
    public AudioSource src;
    private Player playera;
    public VolumeControl volumeControl;
    public Text optionText;
    private bool options=false;
    private GameObject []objetos;

    private TileMap tileMap;
    private void Start()
    {
        
        tileMap = GameObject.Find("TilemapGrid").GetComponent<TileMap>();
        src.clip=sfx1;
        objetos=SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
        foreach(GameObject obj in objetos){
            if(obj.name=="Player"){
                playera = obj.GetComponent<Player>();
                break;
            }
        }
        if(PlayerPrefs.HasKey("autopick")){
            playera.autorecolect=PlayerPrefs.GetInt("autopick");
        }
        if(playera.autorecolect==0){
            optionText.text="Auto Pick Up: OFF";
        }
        else{
            optionText.text="Auto Pick Up: ON";
        }
        
    }
    void Update(){
        if(SceneManager.GetSceneByBuildIndex(0).isLoaded){
            src.Stop();
        }
        if(SceneManager.GetActiveScene()!=SceneManager.GetSceneByBuildIndex(0)){
            if(alive && !isEnding){
                if(music){
                    StartCoroutine(backmusic());
                }
            }
        else{
            if(music && !isEnding){
                StartCoroutine(deadmusic());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)  && !isEnding && !isIntro)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                if(options){
                    BackFromOptions();
                }
                else if (isControlsScreen)
                {
                    BackFromControls();
                }
                Continue();
            }
        }
        }
        
    }

    public void SetIsIntro()
    {
        isIntro = false;
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
            tileMap.ShowRoomsMap();
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
        StartCoroutine(EffectBeforeGameOver());
    }
    
    private IEnumerator EffectBeforeGameOver()
    {
        deadPanel.SetActive(true);
        Image panel = deadPanel.GetComponent<Image>();
        Color panelColor = panel.color;
        
        // Reducir gradualmente la opacidad
        float fadeDuration = 0.5f; // Duración del desvanecimiento en segundos
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; // Incrementar el tiempo transcurrido
            float newOpacity = Mathf.Lerp(0.15f, 0f, elapsed / fadeDuration); // Interpolar opacidad
            panel.color = new Color(panelColor.r, panelColor.g, panelColor.b, newOpacity); 
            yield return null;
        }
        panel.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0f);
        yield return new WaitForSeconds(1f);
    
        deadPanel.SetActive(false);
        gameOverUI.SetActive(true);
        pauseButton.SetActive(false);
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
        tileMap.HideRoomsMap();
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    // Acción del botón de Continuar en la pantalla de pausa
    public void Continue()
    {
        tileMap.HideRoomsMap();
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameObject.Find("PauseButton").GetComponent<Button>().interactable = true;

    }
    // Acción del botón de Opciones en la pantalla de pausa
    public void Options(){
        options=true;
        tileMap.HideRoomsMap();
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void Controls()
    {
        isControlsScreen = true;
        controlsUI.SetActive(true);
        tileMap.HideRoomsMap();
        pauseUI.SetActive(false);
    }
    // Acción del botón de Back en la pantalla de Options
    public void BackFromOptions(){
        if(SceneManager.GetSceneByBuildIndex(0).isLoaded){
            GameObject[] objetos= SceneManager.GetSceneByBuildIndex(0).GetRootGameObjects();
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
            tileMap.ShowRoomsMap();
            pauseUI.SetActive(true);
        }
    }

    // Acción del botón de Back en la pantalla de Controls
    public void BackFromControls()
    {
        isControlsScreen = false;
        controlsUI.SetActive(false);
        tileMap.ShowRoomsMap();
        pauseUI.SetActive(true);
    }
    
    public void GameEnding()
    {
        isEnding = true;
        music=false;
        src.Stop();
        playera.SetIsEnding();
    }
    
    // Acciones de los botones de ajuste de opciones
    public void addVolume(){
        volumeControl.AddVolume();
    }
    public void lessVolume(){
        volumeControl.LessVolume();
    }
    public void addSfx(){
        volumeControl.AddSfx();
    }
    public void lessSfx(){
        volumeControl.LessSfx();
    }
    public void toogleAutoPickUp(){

        playera.autorecolect=playera.autorecolect==0?1:0;
        if(playera.autorecolect==1){
            PlayerPrefs.SetInt("autopick",1);
            optionText.text="Auto Pick Up: ON";
        }
        else{
            PlayerPrefs.SetInt("autopick",0);
            optionText.text="Auto Pick Up: OFF";
        }
    }
    
    
    
}

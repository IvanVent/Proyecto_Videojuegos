using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    
    //comienza una partida comentario editado
    public AudioSource src;
    public GameObject optionsUI;
    public VolumeControl volumeControl;
    public Player player;
    public Text optionText;

    private bool listo=true;
    private bool options=false;
    private Scene scene2;
    GameObject [] objetossegundaescena;
    GameObject canvas;
    GameObject pause;
    GameObject hearts;
    GameObject eventsystem2;
    GameObject [] objetosprimeraescena;

    void Start(){
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }
    
    public void Update(){
        if(listo){
            StartCoroutine(musicamenu());
        }
    }
    private IEnumerator musicamenu()
    {   
        listo=false;
        src.Play();
        yield return new WaitForSeconds(6);
        listo=true;
    }
    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //carga la siguiente escena configurada en el build settings
    }


    //sale del juego
    public void Salir(){
        Debug.Log("Salir");
        Application.Quit();
    }
    public void Opciones(){
        GameObject options=Buscarhijo(canvas,"OptionsScreen");
        options.SetActive(true);
    }
    private GameObject Buscaren2(String quiero){
        GameObject velero=null;
        foreach(GameObject obj in objetossegundaescena){
            if(obj.name==quiero){
                return obj;
            }
        }
        return velero;
    }
    private GameObject Buscaren1(String quiero){
        GameObject velero=null;
        foreach(GameObject obj in objetosprimeraescena){
            if(obj.name==quiero){
                return obj;
            }
        }
        return velero;
    }
    private GameObject Buscarhijo(GameObject padre,String nombrehijo){
        foreach(Transform hijo in padre.transform){
            if(hijo.name==nombrehijo){
                return hijo.gameObject;
            }
        }
        return null;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica que la escena cargada sea la que esperamos
        if (scene.buildIndex == 1)  // Asegúrate de que el buildIndex corresponde a la escena correcta
        {
            Debug.Log("Escena cargada correctamente");

            // Ahora podemos acceder a los objetos de la escena cargada
            objetossegundaescena = scene.GetRootGameObjects();
            objetosprimeraescena = SceneManager.GetSceneByBuildIndex(0).GetRootGameObjects();
            // Imprime los objetos raíz en la escena
            foreach (GameObject obj in objetossegundaescena)
            {
                Debug.Log("Objeto raíz: " + obj.name);
            }
            eventsystem2=Buscaren2("EventSystem");
            eventsystem2.SetActive(false);

            // Busca el Canvas y otros objetos en la escena
            canvas = Buscaren2("Canvas");
            if (canvas != null)
            {
                pause = Buscarhijo(canvas, "PauseButton");
                hearts = Buscarhijo(canvas, "hearts");

                // Desactiva los objetos si están encontrados
                if (hearts != null)
                    hearts.SetActive(false);
                if (pause != null)
                    pause.SetActive(false);
            }
            else
            {
                Debug.LogError("No se encontró el Canvas.");
            }

        }
    }
}

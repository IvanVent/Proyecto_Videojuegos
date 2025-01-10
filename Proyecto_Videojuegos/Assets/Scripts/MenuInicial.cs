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
    GameObject [] objetosintro;
    int escena=-1;

    void Start(){
        escena=2;
        SceneManager.LoadScene(2,LoadSceneMode.Additive);
        objetosprimeraescena=SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();

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
        SceneManager.LoadScene(0,LoadSceneMode.Single);
        /*pause = Buscarhijo(canvas, "PauseButton");
        hearts = Buscarhijo(canvas, "hearts");

        // Desactiva los objetos si están encontrados
        if (hearts != null)
            hearts.SetActive(true);
        if (pause != null)
            pause.SetActive(true);
            */
        
        /*
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        SceneManager.UnloadSceneAsync(escenaactual);
        */

    }


    //sale del juego
    public void Salir(){
        Debug.Log("Salir");
        Application.Quit();
    }
    
    public void Opciones(){
        objetossegundaescena=SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
        GameObject options=Buscarhijo(Buscaren2("Canvas"),"OptionsScreen");
        options.SetActive(true);
        //Buscaren1("EventSystem").SetActive(false);
        Buscaren2("EventSystem").SetActive(true);
        Buscaren1("EventSystem").SetActive(false);
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
    private GameObject Buscarenintro(String quiero){
        foreach(GameObject obj in objetosintro){
            if(obj.name==quiero){
                return obj;
            }
        }
        return null;
    }
    private GameObject Buscarhijo(GameObject padre,String nombrehijo){
        foreach(Transform hijo in padre.transform){
            if(hijo.name==nombrehijo){
                return hijo.gameObject;
            }
        }
        return null;
    }
    
}

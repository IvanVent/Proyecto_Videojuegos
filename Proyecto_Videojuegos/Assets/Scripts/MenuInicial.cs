using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    
    //comienza una partida comentario editado
    public AudioSource src;
    private bool listo=true;
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
}

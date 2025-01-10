using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Gestorescena2 : MonoBehaviour
{
    private GameObject []objetossegundaescena;
    private GameObject []objetosprimeraescena;
    // Start is called before the first frame update
    void Start()
    {
        
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
    void Awake(){
        if(SceneManager.GetActiveScene().name==SceneManager.GetSceneByBuildIndex(1).name){
            objetossegundaescena = SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
                objetosprimeraescena = SceneManager.GetSceneByName("MainMenu").GetRootGameObjects();
                // Imprime los objetos raíz en la escena
                foreach (GameObject obj in objetossegundaescena)
                {
                    Debug.Log("Objeto raíz: " + obj.name);
                }
                Buscaren2("EventSystem").SetActive(false);
               

                // Busca el Canvas y otros objetos en la escena
                Buscarhijo(Buscaren2("Canvas"),"PauseButton").SetActive(false);
                Buscarhijo(Buscaren2("Canvas"),"hearts").SetActive(false);
                
                
                Buscaren2("Player").SetActive(false);
                Buscaren2("Main Camera").SetActive(true);
                SceneManager.LoadScene(0,LoadSceneMode.Additive);
                /*
                objetosintro=SceneManager.GetSceneByName("Intro").GetRootGameObjects();
                foreach (GameObject obj in objetosintro)
                {
                Debug.Log("Objeto raíz: " + obj.name);
                }*/
        }
        else{
            
            Debug.Log("Escena cargada correctamente");

                // ora podemos acceder a los objetos de la escena cargada
            
            
            print("ns");
            objetossegundaescena=SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
            print("rr");
            Buscaren2("Player").SetActive(true);
            print("ss");
            print("Se romperá?");
            Buscaren2("Main Camera").SetActive(true);
            print("aaaaaaaaaaamatmé");
                /*
                objetosintro=SceneManager.GetSceneByName("Intro").GetRootGameObjects();
                foreach (GameObject obj in objetosintro)
                {
                    Debug.Log("Objeto raíz: " + obj.name);
                }
                */
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

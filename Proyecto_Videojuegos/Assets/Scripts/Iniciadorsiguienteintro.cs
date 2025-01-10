using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Iniciadorsiguienteintro : MonoBehaviour
{
    // Start is called before the first frame update
    private int escena=-1;
    private GameObject [] objetossegundaescena;
    private GameObject [] objetosintro;
    void Start()
    {
        objetosintro=SceneManager.GetSceneByBuildIndex(0).GetRootGameObjects();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void siguiente(){
        print("mmmmmm");
        escena=2;
        SceneManager.LoadScene(2,LoadSceneMode.Single);
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
    private GameObject Buscarenintro(String quiero){
        GameObject velero=null;
        foreach(GameObject obj in objetosintro){
            if(obj.name==quiero){
                return obj;
            }
        }
        return velero;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica que la escena cargada sea la que esperamos
        
        if (escena == 2)  // Asegúrate de que el buildIndex corresponde a la escena correcta
        {
            Debug.Log("Escena cargada correctamente");

            // ora podemos acceder a los objetos de la escena cargada
            
            objetossegundaescena = SceneManager.GetSceneByBuildIndex(2).GetRootGameObjects();
            print("ns");
            Buscaren2("Player").SetActive(true);
            Buscarenintro("Main Camera").SetActive(false);
            print("Se romperá?");
            Buscaren2("Main Camera").SetActive(true);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
            print("aaaaaaaaaaamatmé");
            SceneManager.UnloadSceneAsync(0);
            /*
            objetosintro=SceneManager.GetSceneByName("Intro").GetRootGameObjects();
            foreach (GameObject obj in objetosintro)
            {
                Debug.Log("Objeto raíz: " + obj.name);
            }
            */

        }
    }
}

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
    public Image image;
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
    
}

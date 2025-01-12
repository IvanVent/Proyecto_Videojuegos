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
        objetosintro=SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void siguiente(){

        StartCoroutine(transicion());
    }
    IEnumerator transicion()
    {
        Color newcolor=image.color;
        float i=0;
        while(i<=1f){
            newcolor.a=i;
            image.color=newcolor;
            yield return new WaitForSeconds(0.25f);
            i+=0.25f;
        }
        yield return new WaitForSeconds(0.5f);
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

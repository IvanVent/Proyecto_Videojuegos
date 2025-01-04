using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamTrigger : MonoBehaviour
{

    public Vector3 newCamPos, newPlayerPos;

    CamController camControl;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a =SceneManager.GetSceneByBuildIndex(1).GetRootGameObjects();
        foreach(GameObject obj in a){
            if(obj.name=="Main Camera"){
                print("lets goooooo");
                camControl = obj.GetComponent<CamController>();
                break;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            camControl.minPos += newCamPos;
            camControl.maxPos += newCamPos;

            other.transform.position += newPlayerPos;
        }
    }
}

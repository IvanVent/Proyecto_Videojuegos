using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GestorIntro : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    void Start()
    {
        
    }
    void Awake(){

    }
    IEnumerator parabola()
    {
        Color newcolor=image.color;
        float i=0;
        while(i<1f){
            newcolor.a=i;
            image.color=newcolor;
            yield return new WaitForSeconds(0.25f);
            i+=0.25f;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2,LoadSceneMode.Single);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

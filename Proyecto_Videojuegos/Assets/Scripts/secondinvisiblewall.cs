using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondinvisiblewall : MonoBehaviour
{
    // Start is called before the first frame update
    public Playerintro playerintro;
    public Enemigo enemigo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D trigger){
        
        
        
        if(trigger.CompareTag("Playerintro")){
            playerintro.para();
            gameObject.SetActive(false);
            enemigo.actua();
        }
        
    }
}

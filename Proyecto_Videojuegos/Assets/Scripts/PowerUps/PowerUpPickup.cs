using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    // Start is called before the first frame update
    private Player playera;
    public GameObject PopupEUI;
    private GameObject Canvas;
    
    
    public void OnTriggerEnter2D(Collider2D trigger){
        Canvas=GameObject.Find("Canvas");
        foreach (Transform child in Canvas.transform)
        {
            if (child.name == "PressE")
            {
                PopupEUI = child.gameObject;
                break;  // Salimos del bucle cuando encontramos el objeto
            }
        }
        
        playera=GameObject.Find("Player").GetComponent<Player>();
        if(trigger.CompareTag("Player")){
            PopupEUI.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.E)||playera.autorecolect==1){
            if(trigger.CompareTag("Player")){
                PowerUp powerUp=GetComponent<PowerUp>();
                if(powerUp!=null){
                    PopupEUI.SetActive(true);
                    powerUp.OnPick(trigger.gameObject);
                }
            }
        }
    }
    public void OnTriggerStay2D(Collider2D trigger){
        playera=GameObject.Find("Player").GetComponent<Player>();
        if(Input.GetKeyDown(KeyCode.E)||playera.autorecolect==1){
            if(trigger.CompareTag("Player")){
                PowerUp powerUp=GetComponent<PowerUp>();
                if(powerUp!=null){
                    PopupEUI.SetActive(false);
                    powerUp.OnPick(trigger.gameObject);
                }
            }
        }
    }
    public void OnTriggerExit2D(Collider2D trigger){
        PopupEUI.SetActive(false);
    }
}

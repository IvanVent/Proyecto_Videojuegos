using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public bool automatic=false;
    public void OnTriggerEnter2D(Collider2D trigger){
        if(Input.GetKeyDown(KeyCode.E)||automatic){
            if(trigger.CompareTag("Player")){
                PowerUp powerUp=GetComponent<PowerUp>();
                if(powerUp!=null){
                    powerUp.OnPick(trigger.gameObject);
                }
            }
        }
    }
    public void OnTriggerStay2D(Collider2D trigger){
        if(Input.GetKeyDown(KeyCode.E)||automatic){
            if(trigger.CompareTag("Player")){
                PowerUp powerUp=GetComponent<PowerUp>();
                if(powerUp!=null){
                    powerUp.OnPick(trigger.gameObject);
                }
            }
        }
    }
}

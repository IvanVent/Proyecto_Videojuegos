using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D trigger){
        if(trigger.CompareTag("Player")){
            print("aaaaaaaaaaaaaa");
            PowerUp powerUp = GetComponent<PowerUp>();
            if(powerUp!=null){
                print("bbbbbbbbbb");
                powerUp.OnPick(trigger.gameObject);
            }
        }
    }
}

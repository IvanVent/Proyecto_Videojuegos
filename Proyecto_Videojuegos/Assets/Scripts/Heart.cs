using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Heart : PowerUp
{
    // Start is called before the first frame update
    private HeartsGenerator heartmanager;
    public UnityEvent<float,int> vida;
    void Start() { // Encuentra el objeto HeartManager en la escena 
        heartmanager = GameObject.Find("hearts").GetComponent<HeartsGenerator>();  
    }
    public override void ApplyEffect(GameObject player)
    {
        heartmanager.UpdateHearts(1,2);
    }

}

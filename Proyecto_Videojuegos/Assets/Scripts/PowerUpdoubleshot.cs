using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpdoubleshot : PowerUp
{
    // Start is called before the first frame update
    private Player player;
    // Start is called before the first frame update
    
    void Start() { // Encuentra el objeto HeartManager en la escena 
        player = GameObject.Find("Player").GetComponent<Player>();  
    }
    public override void ApplyEffect(GameObject objeto)
    {
        print("aaaaaaaaaaaaaaaaaaaaa");
        player.SetDoubleshot();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpdoubleshot : PowerUp
{
    private Player player;
    private GameProgress gameProgress;
    
    void Start() { // Encuentra el objeto HeartManager en la escena 
        player = GameObject.Find("Player").GetComponent<Player>();  
        gameProgress = GameObject.Find("GameManager").GetComponent<GameProgress>();
    }
    public override void ApplyEffect(GameObject objeto)
    {
        player.SetDoubleshot();
        gameProgress.DobleShotPicked();
    }
}

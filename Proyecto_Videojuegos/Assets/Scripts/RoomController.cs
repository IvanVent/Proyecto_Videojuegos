using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomController : MonoBehaviour
{
    
    private Transform enemiesFather;
    private SmallEnemy enemy1Prefab;
    private BigEnemy enemy2Prefab;
    
    private List<SmallEnemy> smallEnemiesList = new List<SmallEnemy>();
    private List<BigEnemy> bigEnemiesList = new List<BigEnemy>();
    
    void Start()
    {
        
    }

    public void SetEnemiesFather(Transform enemiesFather)
    {
        this.enemiesFather = enemiesFather;
    }

    public void SetSmallEnemyPrefab(SmallEnemy prefab1)
    {
        this.enemy1Prefab = prefab1;
    }

    public void SetBigEnemyPrefab(BigEnemy prefab2)
    {
        this.enemy2Prefab = prefab2;
    }
    
    //crea una instancia de uno de los dos enemigos
    public void AddEnemies(Vector3 pos)
    {
        if (pos.x != 0 && pos.y != 0)
        {
            int n = Random.Range(1, 3);

            if (n == 1)
            {
                SmallEnemy enemy1 = Instantiate(enemy1Prefab, pos, Quaternion.identity);
                enemy1.transform.SetParent(enemiesFather);
                smallEnemiesList.Add(enemy1);
            }
            else
            {
                BigEnemy enemy2 = Instantiate(enemy2Prefab, pos, Quaternion.identity);
                enemy2.transform.SetParent(enemiesFather);
                bigEnemiesList.Add(enemy2);
            }

        }
        
    }
    
    // Activa los enemigos para perseguir al jugador cuando entre en la habitacion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (SmallEnemy e1 in smallEnemiesList)
            {
                e1.EnableFollowPlayer();
            }

            foreach (BigEnemy e2 in bigEnemiesList)
            {
                e2.EnableFollowPlayer();
            }
        }
    }
    
    // Desactiva los enemigos para perseguir al jugador cuando entre en la habitacion
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (SmallEnemy e1 in smallEnemiesList)
            {
                e1.DisableFollowPlayer();
            }

            foreach (BigEnemy e2 in bigEnemiesList)
            {
                e2.DisableFollowPlayer();
            }
        }
    }
}

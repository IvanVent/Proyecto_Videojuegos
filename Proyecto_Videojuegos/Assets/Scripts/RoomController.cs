using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomController : MonoBehaviour
{
    public SmallEnemy enemy1Prefab;
    public BigEnemy enemy2Prefab;
    
    private List<SmallEnemy> smallEnemiesList = new List<SmallEnemy>();
    private List<BigEnemy> bigEnemiesList = new List<BigEnemy>();
    
    private List<GameObject> doorWallColliders = new List<GameObject>();
    private List<GameObject> normalColliders = new List<GameObject>();
    
    public GameObject upDoorPrefab;
    public GameObject downDoorPrefab;
    public GameObject leftDoorPrefab;
    public GameObject rightDoorPrefab;
    
    private Transform enemiesFather; // Objeto vacío donde estarán las instancias de las habitaciones
    private Transform doorsFather; // " " puertas. No será hijo del room para que su trigger no interfiera con el del room

    private int enemyCount = 1;
    private int room_id;
    
    private void Awake()
    {
        LoadColliders();
        enemiesFather = transform.Find("Enemies");
        doorsFather = GameObject.Find("Doors").transform;
    }

    public void SetRoomID(int id)
    {
        this.room_id = id;
    }

    // Guarda los colliders de una habitación en listas para luego acceder a ellos facilmente
    public void LoadColliders()
    {

        var doorWallCollider_obj = transform.Find("DoorColliders");
        var normalCollider_obj = transform.Find("Colliders");

        // Se guardan todos los colliders de las paredes sin puerta y se desactivan. Se activan en
        // AddDoors() solo los que hagan falta.
        foreach (Transform child in doorWallCollider_obj.transform)
        {
            
            doorWallColliders.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        // Se guardan todos los colliders de las paredes con puerta.
        foreach (Transform child in normalCollider_obj.transform)
        {
            normalColliders.Add(child.gameObject);
        }
    }
    
    // Añade las puertas a una habitación, ajustando los colliders de la habitación para adaptarse a ellas.
    public void AddDoors(Vector3 roomPos)
    {
        var doorPos = new Vector3();
        GameObject door;
        
        // Este bucle recorre los bits del id. Dependiendo del bit que esté a 1, se añade una puerta donde corresponde y 
        // se ajustan los colliders.
        for (int i = 0; i < 4; i++)
        {
            int mask = 1 << i;
            if ((room_id & mask) != 0)
            {
                if (i == 0) // puerta arriba
                {
                    doorPos.x = roomPos.x- 2;
                    doorPos.y = roomPos.y + 4;
                    
                    door = Instantiate(upDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                    
                    doorWallColliders[0].gameObject.SetActive(true);
                    normalColliders[0].gameObject.SetActive(false);
                }
                else if (i == 1) // puerta derecha
                {
                    doorPos.x = roomPos.x + 7;
                    doorPos.y = roomPos.y;
                    door = Instantiate(rightDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                    doorWallColliders[1].gameObject.SetActive(true);
                    normalColliders[1].gameObject.SetActive(false);
                }
                else if (i == 2) // puerta abajo
                {
                    doorPos.x = roomPos.x-2;
                    doorPos.y = roomPos.y - 4;
                    door = Instantiate(downDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                    doorWallColliders[2].gameObject.SetActive(true);
                    normalColliders[2].gameObject.SetActive(false);
                }
                else if (i == 3) // puerta izquierda
                {
                    doorPos.x = roomPos.x - 10;
                    doorPos.y = roomPos.y;
                    door = Instantiate(leftDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                    doorWallColliders[3].gameObject.SetActive(true);
                    normalColliders[3].gameObject.SetActive(false);
                }
            }
        }//for
    }//AddDoors
    
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
    
    // Elimina todas las intancias de enemigos, colliders y puertas de la habitacion
    public void ClearRoom()
    {
        // clear de las puertas
        foreach (Transform door in doorsFather.transform)
        {
            Destroy(door.gameObject);
        }
        // clear de los enemigos
        foreach (Transform enemy in enemiesFather.transform)
        {
            Destroy(enemy.gameObject);
        }
        // clear de los colliders
        doorWallColliders.Clear();
        normalColliders.Clear();
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

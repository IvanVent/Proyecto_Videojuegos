using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomController : MonoBehaviour
{
    
    /* Listas de colliders de la habitación */
    private List<GameObject> doorWallColliders = new List<GameObject>();
    private List<GameObject> normalColliders = new List<GameObject>();
    /* Prefabs de las puertas */
    public GameObject upDoorPrefab;
    public GameObject downDoorPrefab;
    public GameObject leftDoorPrefab;
    public GameObject rightDoorPrefab;
    
    /* Objeto vacío donde estarán en la escena las instancias de las puertas */
    private Transform doorsFather;
    
    private int room_id;
    private bool isCompleted = false;
    private EnemySpawner enemy_spawner;
    private Player player;
    
    /* ----------------------------- M E T O D O S -------------------------------------- */
    private void Awake()
    {
        LoadColliders();
        doorsFather = GameObject.Find("Doors").transform;
        enemy_spawner = gameObject.GetComponent<EnemySpawner>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Corutina que va comprobando cuantos enemigos quedan en la sala
    private IEnumerator EnemyCountCheck()
    {
        while (enemy_spawner.GetEnemyCount() > 0)
        {
            yield return new WaitForSeconds(0.08f);
        }
        isCompleted = true;
        OpenDoors();
        yield return null;
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

        // Se guardan todos los colliders de las paredes con los huecos vacios de las puertas 
        foreach (Transform child in doorWallCollider_obj.transform)
        {
            doorWallColliders.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        // Se guardan todos los colliders de las paredes (sin huecos donde las puerta).
        foreach (Transform child in normalCollider_obj.transform)
        {
            normalColliders.Add(child.gameObject);
        }
    }
    
    // Ajusta los colliders para que se puede pasar por las puertas correspondientes 
    public void OpenDoors()
    {
        for (int i = 0; i < 4; i++)
        {
            int mask = 1 << i;
            if ((room_id & mask) != 0)
            {
                if (i == 0) // puerta arriba
                {
                    doorWallColliders[0].gameObject.SetActive(true);
                    normalColliders[0].gameObject.SetActive(false);
                }
                else if (i == 1) // puerta derecha
                {
                    doorWallColliders[1].gameObject.SetActive(true);
                    normalColliders[1].gameObject.SetActive(false);
                }
                else if (i == 2) // puerta abajo
                {
                    doorWallColliders[2].gameObject.SetActive(true);
                    normalColliders[2].gameObject.SetActive(false);
                }
                else if (i == 3) // puerta izquierda
                {
                    doorWallColliders[3].gameObject.SetActive(true);
                    normalColliders[3].gameObject.SetActive(false);
                }
            }
        }
    }
    
    // Añade las puertas a una habitación, ajustando los colliders de la habitación para adaptarse a ellas.
    public void AddDoors(Vector3 roomPos)
    {
        var doorPos = new Vector3();
        GameObject door;
        
        /* Este bucle recorre los bits del id. Dependiendo del bit que esté a 1, se añade una puerta donde  
        corresponde y se ajustan los colliders */
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
                }
                else if (i == 1) // puerta derecha
                {
                    doorPos.x = roomPos.x + 7;
                    doorPos.y = roomPos.y;
                    door = Instantiate(rightDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                }
                else if (i == 2) // puerta abajo
                {
                    doorPos.x = roomPos.x-2;
                    doorPos.y = roomPos.y - 4;
                    door = Instantiate(downDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                }
                else if (i == 3) // puerta izquierda
                {
                    doorPos.x = roomPos.x - 10;
                    doorPos.y = roomPos.y;
                    door = Instantiate(leftDoorPrefab, doorPos, Quaternion.identity);
                    door.transform.SetParent(doorsFather);
                }
            }
        }//for
    }//AddDoors
    
    // Elimina todas las intancias de enemigos, colliders y puertas de la habitacion
    public void ClearRoom()
    {
        // clear de las puertas
        foreach (Transform door in doorsFather.transform)
        {
            Destroy(door.gameObject);
        }
        // clear de los colliders
        doorWallColliders.Clear();
        normalColliders.Clear();
    }
    
    // Cuando el Player entra a la sala se instancian los enemigos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCompleted && player.GetRoomsCompleted()>0)
        {
            enemy_spawner.SpawnEnemies(player.GetRoomsCompleted());
            StartCoroutine("EnemyCountCheck");
        }
    }
    
    // Desactiva los enemigos para perseguir al jugador cuando entre en la habitacion
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCompleted)
        {
            isCompleted = true;
            player.IncrementRoomsCompleted();
        }
    }
}

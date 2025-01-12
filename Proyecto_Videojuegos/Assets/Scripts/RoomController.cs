using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomController : MonoBehaviour
{
    
    /* Lista de instancias de puertas cerradas */
    private List<GameObject> closedDoors = new List<GameObject>();
    
    /* Prefabs de las puertas cerradas */
    [Header("Closed Doors Prefabs")]
    public GameObject closed_upDoorPrefab;
    public GameObject closed_downDoorPrefab;
    public GameObject closed_leftDoorPrefab;
    public GameObject closed_rightDoorPrefab;
    
    /* Prefabs de las puertas abiertas */
    [Header("Open Doors Prefabs")]
    public GameObject open_upDoorPrefab;
    public GameObject open_downDoorPrefab;
    public GameObject open_leftDoorPrefab;
    public GameObject open_rightDoorPrefab;
    
    /* Prefabs de las paredes en los huecos de las puertas */
    [Header("Door Walls Prefabs")]
    public GameObject upDoorWallPrefab;
    public GameObject downDoorWallPrefab;
    public GameObject leftDoorWallPrefab;
    public GameObject rightDoorWallPrefab;

    //POWERUPS
    public GameObject[] powerups;
    public GameObject attackSpeedPU;
    public GameObject damagePU;
    public GameObject movementSpeedPU;
    public GameObject dobleshotPU;
    public GameObject moreHeartsPU;
    public GameObject Potion;
    
    /* Objeto vacío donde estarán en la escena las instancias de las puertas */
    private Transform doorsFather;
    
    /* Propiedades de la sala */
    private int room_id;
    private Vector3 roomPos;
    private bool isCompleted = false;
    private float empty_room_probability = 0.1f; // 10% de probabilidad de sala vacía
    
    private EnemySpawner enemy_spawner;
    private GameProgress game_progress;
    
    /* ----------------------------- M E T O D O S -------------------------------------- */
    private void Awake()
    {
        doorsFather = GameObject.Find("Doors").transform;
        enemy_spawner = gameObject.GetComponent<EnemySpawner>();
        game_progress = GameObject.Find("GameManager").GetComponent<GameProgress>();
        powerups = new GameObject[]{Potion, moreHeartsPU, attackSpeedPU, damagePU, movementSpeedPU, dobleshotPU};
    }

    // Corutina que va comprobando cuantos enemigos quedan en la sala
    private IEnumerator EnemyCountCheck()
    {
        while (enemy_spawner.GetEnemyCount() > 0)
        {
            yield return new WaitForSeconds(0.08f);
        }
        OpenDoors();
        if (!game_progress.GetIsEnding())
        {
            SpawnPowerUp();
        }
        yield return null;
    }
    
    public void SetRoomID(int id)
    {
        this.room_id = id;
    }

    public void SetRoomPos(Vector3 pos)
    {
        this.roomPos = pos;
    }
    
    // Instancia puertas abiertas donde corresponde
    public void OpenDoors()
    {
        game_progress.RoomCompleted();
        isCompleted = true;
        
        if (!game_progress.GetIsEnding())
        {
            var doorPos = new Vector3();
        
            // Se destruyen las instancias de las puertas cerradas
            foreach (var door in closedDoors)
            {
                Destroy(door);
            }
        
            for (int i = 0; i < 4; i++)
            {
                int mask = 1 << i;
                if ((room_id & mask) != 0)
                {
                    if (i == 0) // puerta arriba
                    {
                        doorPos.x = roomPos.x- 2;
                        doorPos.y = roomPos.y + 4;
                        Instantiate(open_upDoorPrefab, doorPos, Quaternion.identity);
                    }
                    else if (i == 1) // puerta derecha
                    {
                        doorPos.x = roomPos.x + 6;
                        doorPos.y = roomPos.y;
                        Instantiate(open_rightDoorPrefab, doorPos, Quaternion.identity);
                    }
                    else if (i == 2) // puerta abajo
                    {
                        doorPos.x = roomPos.x-2;
                        doorPos.y = roomPos.y - 4;
                        Instantiate(open_downDoorPrefab, doorPos, Quaternion.identity);
                    }
                    else if (i == 3) // puerta izquierda
                    {
                        doorPos.x = roomPos.x - 9;
                        doorPos.y = roomPos.y;
                        Instantiate(open_leftDoorPrefab, doorPos, Quaternion.identity);
                    }
                    else
                    {
                        throw new Exception("Invalid bit number of the room id");
                    }
                }
            }
        }
    }
    
    private void SpawnPowerUp()
    {
        int completedRooms = game_progress.GetRoomsCompleted();
        if (completedRooms > 0){
            int random;
            if (completedRooms <= 4){
                random = Random.Range(0, 100);
                if (random < 75){                           //75% de probabilidad de que salga pocion
                    random = 0;
                } else{                                     //25% de probabilidad de que salga corazon extra
                    random = 1;
                }
                GameObject selectedPU = powerups[random];
                GameObject PUinstance = Instantiate(selectedPU, transform.position, Quaternion.identity);
                PUinstance.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }else{
                random = Random.Range(0,100);
                if (random < 70){                           //75% de probabilidad de que salga pocion
                    random = 0;
                }else{                                      //25% de probabilidad de que salga otro PU
                    if(game_progress.GetDobleShotPicked()){ //si ya tiene el dobleShoot no se puede volver a coger
                        random = Random.Range(1, powerups.Length-1);
                    }else{
                        random = Random.Range(1, powerups.Length);
                    }
                }
                GameObject selectedPU = powerups[random];
                GameObject PUinstance = Instantiate(selectedPU, transform.position, Quaternion.identity);
                PUinstance.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
        }
    }

    // Añade las puertas cerradas y las paredes en los huecos correspondientes de la habitación
    public void AddDoors()
    {
        var doorPos = new Vector3();
        GameObject door = null;
        GameObject wall = null;
        
        /* Este bucle recorre los bits del id. Dependiendo del bit que esté a 1, se añade una puerta donde  
        corresponde*/
        for (int i = 0; i < 4; i++)
        {
            int mask = 1 << i;
            if ((room_id & mask) != 0)
            {
                if (i == 0) // puerta arriba
                {
                    doorPos.x = roomPos.x- 2;
                    doorPos.y = roomPos.y + 4;
                    door = Instantiate(closed_upDoorPrefab, doorPos, Quaternion.identity);
                }
                else if (i == 1) // puerta derecha
                {
                    doorPos.x = roomPos.x + 5;
                    doorPos.y = roomPos.y;
                    door = Instantiate(closed_rightDoorPrefab, doorPos, Quaternion.identity);
                }
                else if (i == 2) // puerta abajo
                {
                    doorPos.x = roomPos.x-2;
                    doorPos.y = roomPos.y - 4;
                    door = Instantiate(closed_downDoorPrefab, doorPos, Quaternion.identity);
                }
                else if (i == 3) // puerta izquierda
                {
                    doorPos.x = roomPos.x - 9;
                    doorPos.y = roomPos.y;
                    door = Instantiate(closed_leftDoorPrefab, doorPos, Quaternion.identity);
                    
                }
                else
                {
                    throw new Exception("Invalid bit number of the room id");
                }
                door.transform.SetParent(doorsFather);
                closedDoors.Add(door);
            }
            else // Si no hay una puerta, se pone la pared
            {
                if (i == 0) // pared arriba
                {
                    doorPos.x = roomPos.x- 2;
                    doorPos.y = roomPos.y + 4;
                    wall = Instantiate(upDoorWallPrefab, doorPos, Quaternion.identity);
                }
                else if (i == 1) // pared derecha
                {
                    doorPos.x = roomPos.x + 5;
                    doorPos.y = roomPos.y;
                    wall = Instantiate(rightDoorWallPrefab, doorPos, Quaternion.identity);
                }
                else if (i == 2) // pared abajo
                {
                    doorPos.x = roomPos.x-2;
                    doorPos.y = roomPos.y - 4;
                    wall = Instantiate(downDoorWallPrefab, doorPos, Quaternion.identity);
                }
                else if (i == 3) // pared izquierda
                {
                    doorPos.x = roomPos.x - 9;
                    doorPos.y = roomPos.y;
                    wall = Instantiate(leftDoorWallPrefab, doorPos, Quaternion.identity);
                    
                }
                else
                {
                    throw new Exception("Invalid bit number of the room id");
                }
                wall.transform.SetParent(doorsFather);
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
    }
    
    // Cuando el Player entra a la sala se instancian los enemigos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCompleted && game_progress.GetRoomsCompleted()>0)
        {
            float randomValue = Random.value;
            if (randomValue < empty_room_probability)
            {
                OpenDoors();
                return;
            }
            enemy_spawner.SpawnEnemies(game_progress.GetRoomsCompleted());
            StartCoroutine("EnemyCountCheck");
        }
    }
    
    
}

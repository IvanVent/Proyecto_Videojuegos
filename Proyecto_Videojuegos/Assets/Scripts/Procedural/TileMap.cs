using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class TileMap : MonoBehaviour
{
    public GameObject roomPrefab;
    /*
    public GameObject upDoorPrefab;
    public GameObject downDoorPrefab;
    public GameObject leftDoorPrefab;
    public GameObject rightDoorPrefab;
    */
    //public SmallEnemy enemy1Prefab;
    //public BigEnemy enemy2Prefab;

    private GameObject room;

    public Transform roomsFather; // Objeto vacío donde estarán las instancias de las habitaciones
    
    //private List<GameObject> doorWallColliders = new List<GameObject>();
    //private List<GameObject> normalColliders = new List<GameObject>();
    float roomWidth = 9f;
    float roomHeight = 10f;
    
    private RoomController roomController;

    private void Start()
    {
        ClearTileMap();
    }
    
    // Este método se llama cada vez que se recorre una celda de la matriz que define el mapa.
    public void AddRoom(float x, float y, int id = 0)
    {
        var roomPos = new Vector3(x * roomWidth * 2, y * roomHeight, 1f);
        
        /* Si la habitación actual no tiene puertas (id == 0) no se instancia la habitación, es decir, 
        hay un hueco vacío en el mapa. */
        if (id != 0)
        {
            room = Instantiate(roomPrefab, roomPos, Quaternion.identity);
            room.transform.SetParent(roomsFather);
            
            roomController = room.GetComponent<RoomController>();
            roomController.SetRoomID(id);
            roomController.AddEnemies(new Vector3(roomPos.x - 4, roomPos.y - 1, 0));
            roomController.AddDoors(roomPos);
        }
    }
    
    // elimina todas las habitaciones, enemigos, puertas y colliders
    public void ClearTileMap()
    {
        // clear de las habitaciones
        foreach (Transform roomObj in roomsFather.transform)
        {
            Destroy(room.gameObject);
        }
        //roomController.ClearRoom();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;


public class TileMap : MonoBehaviour
{
    public GameObject roomPrefab;
    public Transform roomsFather; // Objeto vacío donde estarán las instancias de las habitaciones
    public GameObject[] floorPrefabs;
    public GameObject canvas;
    public GameObject roomInMapPrefab;
    public GameObject roomInMapColorPrefab;

    //private GameObject[] rooms;
    private List<GameObject> roomsInMap = new List<GameObject>();
    private int listsPos = 0;
    
    Vector3 initialRoomInMapPos = new Vector3(252f, 90f, 0f);
    
    float roomWidth = 9f;
    float roomHeight = 10f;
    
    // Variables para ajustar el tamaño y espacio del mapa en el canvas
    float roomInMapWidth = 24f;  
    float roomInMapHeight = 22f; 
    
    private RoomController roomController;
    private bool isFirst = true;

    private int totalRooms;

    private void Start()
    {
        ClearTileMap();
        floorPrefabs = Resources.LoadAll<GameObject>("Floors");
    }

    public RoomController GetRoomController()
    {
        return roomController;
    }

    public void SetRoomsNumber(int n)
    {
        totalRooms = n;
    }
    
    // Este método se llama cada vez que se recorre una celda de la matriz que define el mapa.
    public void AddRoom(float x, float y, int id = 0)
    {
        // Multiplicadores para mantener la separación relativa de las habitaciones en el mapa
        float xOffset = x * roomInMapWidth; // Ajuste horizontal para los cuadrados
        float yOffset = y * roomInMapHeight;   // Ajuste vertical para los cuadrados
        
        var roomPos = new Vector3(x * roomWidth * 2, y * roomHeight, 1f);
        
        var roomInMapPos = new Vector3(2 * xOffset + 190, 2 * yOffset - 95, 0f);
        
        /* Si la habitación actual no tiene puertas (id == 0) no se instancia la habitación, es decir, 
        hay un hueco vacío en el mapa. */
        if (id != 0)
        {
            //instancia el room
            GameObject room = Instantiate(roomPrefab, roomPos, Quaternion.identity);
            
            GameObject roomInMap = Instantiate(roomInMapPrefab, canvas.transform);
            RectTransform squareRect = roomInMap.GetComponent<RectTransform>();
            squareRect.anchoredPosition = roomInMapPos;
            roomInMap.SetActive(false);
            roomsInMap.Add(roomInMap);
            
            //instancia el floor del room
            GameObject floorPrefab = floorPrefabs[Random.Range(0, floorPrefabs.Length)];
            GameObject floor = Instantiate(floorPrefab, roomPos, Quaternion.identity);
            
            //guarda las instancias en el empty object father
            room.transform.SetParent(roomsFather);
            floor.transform.SetParent(roomsFather);
            
            //prepara el room
            roomController = room.GetComponent<RoomController>();
            roomController.SetRoomID(id);
            roomController.SetRoomPos(roomPos);
            roomController.SetRoomListPos(listsPos);
            roomController.AddDoors();
            if (isFirst)
            {
                roomController.OpenDoors();
                isFirst = false;
            }
            listsPos++;
        }
    }

    public void UpdateMap(int pos)
    {
        print("POS: " + pos);
        roomsInMap[pos].transform.Find("colorBG").GetComponent<Image>().color = Color.green;
    }

    public void ShowRoomsMap()
    {
        foreach (GameObject room in roomsInMap)
        {
            room.SetActive(true);
        }
    }
    public void HideRoomsMap()
    {
        foreach (GameObject room in roomsInMap)
        {
            room.SetActive(false);
        }
    }
    // elimina todas las habitaciones, enemigos, puertas y colliders
    public void ClearTileMap()
    {
        // clear de las habitaciones
        foreach (Transform roomObj in roomsFather.transform)
        {
            Destroy(roomObj.gameObject);
        }
    }

}

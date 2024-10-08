using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TileMap : MonoBehaviour
{
    public GameObject roomPrefab;
    public GameObject doorPrefab;
    private GameObject room;
    private List<GameObject> doorWallColliders;
    private List<GameObject> normalColliders;
    float roomWidth = 9f;
    float roomHeight = 10f;

    private void Start()
    {

    }
    // Este método se llama cada vez que se recorre una celda de la matriz que define el mapa.
    public void AddRoom(float x, float y, int id = 0)
    {
        var roomPos = new Vector3(x * roomWidth * 2, y * roomHeight, 1f);
        
        // Si la habitación actual no tiene puertas (id == 0) no se instancia la habitación, es decir, 
        // hay un hueco vacío en el mapa.
        if (id != 0)
        {
            room = Instantiate(roomPrefab, roomPos, Quaternion.identity);
            
            var doorWallCollider_obj = room.transform.Find("DoorColliders");
            var normalCollider_obj = room.transform.Find("Colliders");
            
            doorWallColliders = new List<GameObject>();
            normalColliders = new List<GameObject>();
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
            AddDoors(id, roomPos);
        }

    }

    // Método que añade las puertas a una habitación, ajustando los colliders de la habitación para adaptarse a ellas.
    private void AddDoors(int id, Vector3 roomPos)
    {
        var doorPos = new Vector3();
        
        // Este bucle recorre los bits del id. Dependiendo del bit que esté a 1, se añade una puerta donde corresponde y 
        // se ajustan los colliders.
        for (int i = 0; i < 4; i++)
        {
            int mask = 1 << i;
            if ((id & mask) != 0)
            {
                if (i == 0) // puerta arriba
                {
                    doorPos.x = roomPos.x;
                    doorPos.y = roomPos.y + 4;
                    Instantiate(doorPrefab, doorPos, Quaternion.identity);
                    doorWallColliders[0].gameObject.SetActive(true);
                    normalColliders[0].gameObject.SetActive(false);
                }
                else if (i == 1) // puerta derecha
                {
                    doorPos.x = roomPos.x + 7;
                    doorPos.y = roomPos.y;
                    Instantiate(doorPrefab, doorPos, Quaternion.identity);
                    doorWallColliders[1].gameObject.SetActive(true);
                    normalColliders[1].gameObject.SetActive(false);
                }
                else if (i == 2) // puerta abajo
                {
                    doorPos.x = roomPos.x;
                    doorPos.y = roomPos.y - 4;
                    Instantiate(doorPrefab, doorPos, Quaternion.identity);
                    doorWallColliders[2].gameObject.SetActive(true);
                    normalColliders[2].gameObject.SetActive(false);
                }
                else if (i == 3) // puerta izquierda
                {
                    doorPos.x = roomPos.x - 10;
                    doorPos.y = roomPos.y;
                    Instantiate(doorPrefab, doorPos, Quaternion.identity);
                    doorWallColliders[3].gameObject.SetActive(true);
                    normalColliders[3].gameObject.SetActive(false);
                }
            }
        }//for
    }//AddDoors



    

}

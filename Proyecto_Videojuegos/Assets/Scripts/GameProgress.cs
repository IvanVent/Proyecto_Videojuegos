using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private int roomsCompleted = 0;
    private int totalRooms;
    private int enemiesKilled = 0;
    
    private bool dobleShotPU_picked = false;
    private bool isEnding = false;

    private GameManager gameManager;
    
    public TileMap tileMap;

    void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
        tileMap = GameObject.Find("TilemapGrid").GetComponent<TileMap>();
    }
    public void RoomCompleted()
    {
        roomsCompleted++;
        if (roomsCompleted == totalRooms)
        {
            isEnding= true;
            gameManager.GameEnding();
        }
    }

    public void UpdateMap(int roomMapPos)
    {
        tileMap.UpdateMap(roomMapPos);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    public void DobleShotPicked()
    {
        dobleShotPU_picked = true;
    }

    public bool GetIsEnding()
    {
        return isEnding;
    }

    public int GetRoomsCompleted()
    {
        return roomsCompleted;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public bool GetDobleShotPicked()
    {
        return dobleShotPU_picked;
    }

    public void SetTotalRooms(int rooms)
    {
        totalRooms = rooms;
    }
}

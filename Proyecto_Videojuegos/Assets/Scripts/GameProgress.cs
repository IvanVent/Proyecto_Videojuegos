using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private int roomsCompleted = -1; // -1 para que no cuente la sala de inicio como completada
    private int enemiesKilled = 0;

    public void RoomCompleted()
    {
        roomsCompleted++;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    public int GetRoomsCompleted()
    {
        Debug.Log("Rooms completed: " + roomsCompleted);
        return roomsCompleted;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private int roomsCompleted = 0; // -1 para que no cuente la sala de inicio como completada
    private int enemiesKilled = 0;

    private bool dobleShotPU_picked = false;

    public void RoomCompleted()
    {
        roomsCompleted++;
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }

    public void DobleShotPicked()
    {
        dobleShotPU_picked = true;
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

    public bool GetDobleShotPicked()
    {
        return dobleShotPU_picked;
    }
}

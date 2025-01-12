using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRandom : MonoBehaviour
{

    [SerializeField] private int maxStep = 3;
    [SerializeField] private int maxX = 4;
    [SerializeField] private int maxY = 4;
    [SerializeField] private int emptyCells = 4;

    MemTurtle turtle;
    MemMaze maze;
    TileMap tileMap;
    GameProgress gameProgress;

    void Start()
    {
       Application.targetFrameRate = 30;
       tileMap = GameObject.Find("TilemapGrid").GetComponent<TileMap>();
       gameProgress = GetComponentInParent<GameProgress>();
       
       gameProgress.SetTotalRooms((maxX * maxY) - emptyCells);
       
       turtle = new MemTurtle();
       turtle.SetClamp(maxX, maxY);
       turtle.forwardDelegate = (pos, lastPos, turn, invTurn) =>
       {
           maze.Add(pos, lastPos, turn, invTurn);
       };
       
       maze = new MemMaze();
       maze.iteratorDelegate = (x, y, value) =>
       {
           tileMap.AddRoom(x, y, value);
       };
       Walk();
    }

    void Restart()
    {
        maze.Clear();
        maze.Fill(maxX, maxY);
    }

    void UpdateTilemap()
    {
        maze.IterateRect();
    }

    // Limpia la matriz, genera un nuevo mapa aleatorio de habitaciones y llama a UpdateTilemap() para
    // recorrer la matriz e instanciar las habitaciones y sus puertas.
    public void Walk()
    {
        Restart();
        while (maze.GetEmptyCount() > emptyCells)
        {
            turtle.TurnTo(Random.Range(0, 4));
            turtle.Forward(Random.Range(1, maxStep +1));
        }
        UpdateTilemap();
    }
    













}

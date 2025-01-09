using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private string easyPrefabsPath = "SpawnsPrefabs/Easy";
    private string mediumPrefabsPath = "SpawnsPrefabs/Medium";

    public SmallEnemy smallEnemy_prefab;
    public BigEnemy bigEnemy_prefab;
    private GameProgress game_progress;

    private Transform enemiesFather;
    private List<SmallEnemy> small_list = new List<SmallEnemy>();
    private List<BigEnemy> big_list = new List<BigEnemy>();
    private Vector3 room_pos;
    private int enemy_count = 0;
    
    private float time_to_follow = 0.75f; //tiempo que tarda el enemigo en empezar a seguirte 
    
    /* Número de salas que deben ser completadas para que empiecen a spawnear
    los enemigos en spawns de dificultad medium */
    private int rooms_to_medium = 5;
    
    /* ----------------------------- M E T O D O S -------------------------------------- */

    private void Awake()
    {
        enemiesFather = transform.Find("Enemies").gameObject.transform;
        room_pos = gameObject.transform.position;
        game_progress = GameObject.Find("GameManager").GetComponent<GameProgress>();
    }

    // Devuelve cuantos enemigos quedan vivos
    public int GetEnemyCount()
    {
        small_list.RemoveAll(small => small == null);
        big_list.RemoveAll(big => big == null);
        return small_list.Count + big_list.Count;
    }

    // Devuelve el objeto de un prefab de spawn aleatorio de dificultad easy
    private GameObject LoadEasyPrefab()
    {
        GameObject[] easySpawns = Resources.LoadAll<GameObject>(easyPrefabsPath);
        return easySpawns[Random.Range(0, easySpawns.Length)];
    }

    // Devuelve el objeto de un prefab de spawn aleatorio de dificultad medium 
    private GameObject LoadMediumPrefab()
    {
        GameObject[] mediumSpawns = Resources.LoadAll<GameObject>(mediumPrefabsPath);
        return mediumSpawns[Random.Range(0, mediumSpawns.Length)];
    }

    // Carga un prefab de la dificultad que toque e instancia los enemigos
    public void SpawnEnemies(int rooms_completed)
    {
        GameObject spawn_prefab;

        if (rooms_completed < rooms_to_medium) 
        {
            spawn_prefab = LoadEasyPrefab();    // easy
        }
        else
        {
            spawn_prefab = LoadMediumPrefab();  // medium
        }
        InstanciateEnemies(spawn_prefab);
    }

    // Crea las instancias de los enemigos en base a un prefab de spawn dado
    private void InstanciateEnemies(GameObject spawn_prefab)
    {
        // Instancia un enemigo en cada posición definida por el prefab
        foreach (Transform spawnPoint in spawn_prefab.transform)
        {
            enemy_count++;
            int enemiesKilled = game_progress.GetEnemiesKilled();
            if (spawnPoint.name.Contains("Small"))
            {
                SmallEnemy small_e = Instantiate(smallEnemy_prefab, spawnPoint.position + room_pos, Quaternion.identity);
                small_e.transform.SetParent(enemiesFather);
                if (enemiesKilled > 15)
                {
                    small_e.setHP(4);
                    small_e.setSpeed(8f);
                    small_e.setCooldownParada(1f);
                    small_e.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
                }
                small_list.Add(small_e);
                StartCoroutine("SmallWaitToFollow", small_e);
            }
            else if (spawnPoint.name.Contains("Big"))
            {
                BigEnemy big_e = Instantiate(bigEnemy_prefab, spawnPoint.position + room_pos, Quaternion.identity);
                big_e.transform.SetParent(enemiesFather);
                if(enemiesKilled > 15)
                {
                    big_e.setHP(10);
                    big_e.setSpeed(1.8f);
                    big_e.transform.localScale = new Vector3(0.075f, 0.075f, 0.075f);
                }
                big_list.Add(big_e);
                StartCoroutine("BigWaitToFollow", big_e);
            }
        }
    }

    // corutinas para que los enemigos se esperen antes de perseguir al player
    private IEnumerator SmallWaitToFollow(SmallEnemy enemy)
    {
        yield return new WaitForSeconds(time_to_follow);
        enemy.EnableFollowPlayer();
    }
    private IEnumerator BigWaitToFollow(BigEnemy enemy)
    {
        yield return new WaitForSeconds(time_to_follow);
        enemy.EnableFollowPlayer();
    }

    
    
}

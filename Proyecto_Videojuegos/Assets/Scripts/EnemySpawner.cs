using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private string easyPrefabsPath = "SpawnsPrefabs/Easy";
    private string mediumPrefabsPath = "SpawnsPrefabs/Medium";

    public SmallEnemy smallEnemy_prefab;
    public BigEnemy bigEnemy_prefab;

    private Transform enemiesFather;

    private Vector3 room_pos;
    /* Número de salas que deben ser completadas para que empiecen a spawnear
    los enemigos en spawns de dificultad medium */
    private int rooms_to_medium = 5;
    
    /* ----------------------------- M E T O D O S -------------------------------------- */

    private void Awake()
    {
        enemiesFather = transform.Find("Enemies").gameObject.transform;
        room_pos = gameObject.transform.position;
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
            if (spawnPoint.name.Contains("Small"))
            {
                SmallEnemy small_e = Instantiate(smallEnemy_prefab, spawnPoint.position + room_pos, Quaternion.identity);
                small_e.transform.SetParent(enemiesFather);
                StartCoroutine("SmallWaitToFollow", small_e);
            }
            else if (spawnPoint.name.Contains("Big"))
            {
                BigEnemy big_e = Instantiate(bigEnemy_prefab, spawnPoint.position + room_pos, Quaternion.identity);
                big_e.transform.SetParent(enemiesFather);
                StartCoroutine("BigWaitToFollow", big_e);
            }
        }
    }


    private IEnumerator SmallWaitToFollow(SmallEnemy enemy)
    {
        yield return new WaitForSeconds(0.75f);
        enemy.EnableFollowPlayer();
    }
    private IEnumerator BigWaitToFollow(BigEnemy enemy)
    {
        yield return new WaitForSeconds(0.75f);
        enemy.EnableFollowPlayer();
    }

    
    
}

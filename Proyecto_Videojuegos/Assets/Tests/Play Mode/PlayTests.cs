using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayTests
{

    // Test para comprobar que el enemigo pequeño pierde vida al recibir daño
    [UnityTest]
    public IEnumerator SE_TakeDamage()
    {
        SceneManager.LoadScene("SampleScene");
        yield return null;
        
        var smallEnemy = new GameObject().AddComponent<SmallEnemy>();
        yield return null;
        
        float hpLeft = smallEnemy.getHP() - 1;
        
        smallEnemy.TakeDamage();
        Assert.AreEqual(hpLeft, smallEnemy.getHP());
    }
    
    // Test para comprobar que el enemigo grande pierde vida al recibir daño
    [UnityTest]
    public IEnumerator BE_TakeDamage()
    {
        SceneManager.LoadScene("SampleScene");
        yield return null;
        
        var bigEnemy = new GameObject().AddComponent<BigEnemy>();
        yield return null;
        
        float hpLeft = bigEnemy.getHP() - 1;

        bigEnemy.TakeDamage();
        Assert.AreEqual(hpLeft, bigEnemy.getHP());
    }
    
    // Comprueba que el metodo LoadColliders de RoomController guarde correctamente los colliders en las listas
    [UnityTest]
    public IEnumerator RoomLoadColliders()
    {
        SceneManager.LoadScene("SampleScene");
        yield return null;

        var tileMap = new GameObject().AddComponent<TileMap>();
        yield return null;
        
        tileMap.AddRoom(0,0,0);
        yield return null;
        
        var room_controller = tileMap.GetRoomController();
        Assert.AreEqual(4, room_controller.GetNormalColliders().Count);
        Assert.AreEqual(4, room_controller.GetDoorWallColliders().Count);
    }
    
    
    
}

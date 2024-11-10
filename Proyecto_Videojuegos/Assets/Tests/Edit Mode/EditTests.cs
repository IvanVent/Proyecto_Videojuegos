using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
public class EditTests
{
    // Test para comprobar que el enemigo pequeño pierde vida al recibir daño
    [Test]
    public void SE_TakeDamage()
    {
        var smallEnemy = new GameObject().AddComponent<SmallEnemy>();
        float hpLeft = smallEnemy.getHP() - 1;

        smallEnemy.TakeDamage();
        Assert.AreEqual(hpLeft, smallEnemy.getHP());
    }

    // Test para comprobar que el enemigo grande pierde vida al recibir daño
    [Test]
    public void BE_TakeDamage()
    {
        var bigEnemy = new GameObject().AddComponent<BigEnemy>();
        float hpLeft = bigEnemy.getHP() - 1;

        bigEnemy.TakeDamage();
        Assert.AreEqual(hpLeft, bigEnemy.getHP());
    }
    


}

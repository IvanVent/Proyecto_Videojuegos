
using NUnit.Framework;
using UnityEngine;

public class EditTests
{
    // Test para comprobar que el enemigo pequeño pierde vida al recibir daño
    /*[Test]
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
    */

    [Test]
    public void AttackSpeedIncrease()
    {
        var player = new GameObject().AddComponent<Player>();
        float inicialShootCD = player.getShootCooldown();

        player.DecreaseShootCooldown();

        Assert.AreNotEqual(inicialShootCD, player.getShootCooldown());
    }

    [Test]
    public void DamageIncrease()
    {
        var player = new GameObject().AddComponent<Player>();
        float inicialDamage = player.getdamage();

        player.IncreaseDamage();

        Assert.AreNotEqual(inicialDamage, player.getdamage());
    }

    [Test]
    public void SpeedIncrease()
    {
        var player = new GameObject().AddComponent<Player>();
        float inicialSpeed = player.getVelocidad();

        player.IncreaseSpeed();

        Assert.AreNotEqual(inicialSpeed, player.getVelocidad());
    }

    [Test]
    public void DoubleShot()
    {
        var player = new GameObject().AddComponent<Player>();
        bool inicialDoubleShot = player.getDobleShot();

        player.SetDoubleshot();

        Assert.AreNotEqual(inicialDoubleShot, player.getDobleShot());
    }

}

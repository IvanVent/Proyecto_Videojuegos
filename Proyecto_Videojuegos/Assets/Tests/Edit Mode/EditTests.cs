
using NUnit.Framework;
using UnityEngine;

public class EditTests
{

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
        float inicialDamage = player.getDamage();

        player.IncreaseDamage();

        Assert.AreNotEqual(inicialDamage, player.getDamage());
    }

    [Test]
    public void SpeedIncrease()
    {
        var player = new GameObject().AddComponent<Player>();
        float inicialSpeed = player.getSpeed();

        player.IncreaseSpeed();

        Assert.AreNotEqual(inicialSpeed, player.getSpeed());
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup
{
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;
    public float fireRateModifier;

    public float duration;

    public bool isPermanent;

    public void OnActivate(TankData target)
    {
        target.moveSpeed += speedModifier;
        target.Health += healthModifier;
        target.MaxHealth += maxHealthModifier;
        target.fireRate += fireRateModifier;
    }

    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedModifier;
        target.Health -= healthModifier;
        target.MaxHealth -= maxHealthModifier;
        target.fireRate -= fireRateModifier;
    }
}

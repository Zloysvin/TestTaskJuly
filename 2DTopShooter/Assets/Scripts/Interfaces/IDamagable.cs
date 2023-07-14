using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public float HP { get; set; }
    public float HPMax { get; set; }

    public event EventHandler<DamageTakenEventArgs> OnDamageTaken; 

    public event EventHandler<DeathEventArgs> OnDeath;

    public void ReceiveDamage(float damage, bool IsShootingPlayer);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenEventArgs : EventArgs
{
    public float Hp { get; set; }
    public float MaxHp { get; set; }
    public float Damage { get; set; }

    public DamageTakenEventArgs(float hp, float maxHp, float damage)
    {
        Hp = hp;
        MaxHp = maxHp;
        Damage = damage;
    }
}

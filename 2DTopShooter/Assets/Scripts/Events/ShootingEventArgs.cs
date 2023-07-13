using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEventArgs : EventArgs
{
    public int AmmoInClip { get; set; }
    public int RestAmmo { get; set; }

    public ShootingEventArgs(int ammoInClip, int restAmmo)
    {
        AmmoInClip = ammoInClip;
        RestAmmo = restAmmo;
    }
}

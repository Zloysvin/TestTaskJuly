using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEventArgs : EventArgs
{
    public bool KilledByPlayer { get; set; }

    public DeathEventArgs(bool killedByPlayer)
    {
        KilledByPlayer = killedByPlayer;
    }
}

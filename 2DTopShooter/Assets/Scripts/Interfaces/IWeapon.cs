using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    string Name { get; set; }
    Transform GunPoint { set; }
    Projectile Projectile { set; }

    public int AmmoClipSize { get; set; }
    public int CurrentAmmoInClip { get; set; }
    public int CurrentAmmo { get; set; }
    public int MaxAmmo { get; set; }
    public float ShootTime { set; }
    public float ReloadTime { set; }

    public bool Shooting { get; set; }
    public bool Reloading { get; set; }

    public void Shoot();

    public void Reload();

    public IEnumerator ShootDelay();

    public IEnumerator ReloadDelay();
}

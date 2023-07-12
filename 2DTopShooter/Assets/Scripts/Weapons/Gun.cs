using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Transform GunPoint { get; set; }
    [field: SerializeField] public Projectile Projectile { get; set; }
    [field: SerializeField] public int AmmoClipSize { get; set; }
    [field: SerializeField] public int CurrentAmmoInClip { get; set; }
    [field: SerializeField] public int CurrentAmmo { get; set; }
    [field: SerializeField] public int MaxAmmo { get; set; }
    [field: SerializeField] public float ShootTime { get; set; }
    [field: SerializeField] public float ReloadTime { get; set; }
    public bool Shooting { get; set; }
    public bool Reloading { get; set; }

    public void Shoot()
    {
        if (!Shooting)
            StartCoroutine(ShootDelay());
    }

    public void Reload()
    {
        if(!Reloading)
            StartCoroutine(ReloadDelay());
    }

    public IEnumerator ShootDelay()
    {
        if (CurrentAmmoInClip > 0)
        {
            Shooting = true;
            CurrentAmmo--;
            CurrentAmmoInClip--;
            Projectile bullet = Instantiate(Projectile, GunPoint.position, transform.rotation);
            bullet.Project(transform.up);
            yield return new WaitForSeconds(ShootTime);
            Shooting = false;
        }
        else
        {
            Reload();
        }
    }

    public IEnumerator ReloadDelay()
    {
        Reloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(ReloadTime);
        CurrentAmmoInClip = AmmoClipSize;
        Reloading = false;
    }
}

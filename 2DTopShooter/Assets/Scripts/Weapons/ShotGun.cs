using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotGun : MonoBehaviour, IWeapon
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Transform GunPoint { get; set; }
    [field: SerializeField] public Projectile Projectile { get; set; }
    public event EventHandler<ShootingEventArgs> OnShoot;
    [field: SerializeField] public bool IsAutomatic { get; set; }
    [field: SerializeField] public int AmmoClipSize { get; set; }
    [field: SerializeField] public int CurrentAmmoInClip { get; set; }
    [field: SerializeField] public int CurrentAmmo { get; set; }
    [field: SerializeField] public int MaxAmmo { get; set; }
    [field: SerializeField] public Sprite Ammo { get; set; }
    [field: SerializeField] public float ShootTime { get; set; }
    [field: SerializeField] public float ReloadTime { get; set; }
    [field: SerializeField][field: Range(0.0f, 5.0f)] public float SpreadMultiplier { get; set; }
    public bool Shooting { get; set; }
    public bool Reloading { get; set; }
    public void Shoot()
    {
        if (!Shooting)
            StartCoroutine(ShootDelay());
    }

    public void Reload()
    {
        if (!Reloading)
            StartCoroutine(ReloadDelay());
    }

    public IEnumerator ShootDelay()
    {
        if (CurrentAmmoInClip > 0)
        {
            Shooting = true;
            CurrentAmmo--;
            CurrentAmmoInClip--;
            OnShoot(this, new ShootingEventArgs(CurrentAmmoInClip, CurrentAmmo - CurrentAmmoInClip));
            for (int i = 0; i < 8; i++)
            {
                Projectile bullet = Instantiate(Projectile, GunPoint.position, transform.rotation);
                bullet.Project(transform.up + new Vector3(Random.Range(-0.1f * SpreadMultiplier, 0.1f * SpreadMultiplier),
                    Random.Range(-0.1f * SpreadMultiplier, 0.1f * SpreadMultiplier)));
            }

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
        CurrentAmmoInClip = CurrentAmmo >= AmmoClipSize ? AmmoClipSize : CurrentAmmo;
        OnShoot(this, new ShootingEventArgs(CurrentAmmoInClip, CurrentAmmo - CurrentAmmoInClip));
        Reloading = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _camera;
    private IWeapon _currentWeapon;

    private Vector2 _moveDirection;
    private int _selectedWeaponIndex;
    private float _fadeInTime;
    private float _fadeOutTime;

    [SerializeField] private float _speed;
    [SerializeField] private Image _ammoType;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _reload;

    public GameObject[] Weapons;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        EquipWeapon();

        foreach (var weapon in Weapons)
        {
            weapon.GetComponent<IWeapon>().OnShoot += PlayerController_OnShoot;
            weapon.GetComponent<IWeapon>().OnReload += PlayerController_OnReload;
        }

        _ammoText.text = $"{_currentWeapon.CurrentAmmoInClip}/{_currentWeapon.CurrentAmmo - _currentWeapon.CurrentAmmoInClip}";
    }

    private void PlayerController_OnReload(object sender, System.EventArgs e)
    {
        StartCoroutine(ReloadAnimation(_currentWeapon.ReloadTime));
    }

    private void PlayerController_OnShoot(object sender, ShootingEventArgs e)
    {
        _ammoText.text = $"{e.AmmoInClip}/{e.RestAmmo}";
    }

    void Update()
    {
        LookAtMouse();

        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");

        if( _currentWeapon.IsAutomatic)
        {
            if (Input.GetMouseButton(0))
                _currentWeapon.Shoot();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                _currentWeapon.Shoot();
        }

        if (Input.GetMouseButton(1))
        {
            _currentWeapon.Reload();
        }

        if (Input.GetKeyDown(KeyCode.Q))
            SwapWeapons(_selectedWeaponIndex - 1);

        if (Input.GetKeyDown(KeyCode.E))
            SwapWeapons(_selectedWeaponIndex + 1);
    }

    void FixedUpdate()
    {
        Move();
    }

    void LookAtMouse()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    void Move()
    {
        _rb.MovePosition(_rb.position + _moveDirection * _speed * Time.fixedDeltaTime);
    }

    void EquipWeapon()
    {
        _currentWeapon = GetComponentInChildren<IWeapon>();
        _ammoType.sprite = _currentWeapon.Ammo;
        Debug.Log(_currentWeapon.Name);
    }

    void SwapWeapons(int newIndex)
    {
        if (newIndex >= Weapons.Length)
            newIndex = 0;
        if(newIndex < 0)
            newIndex = Weapons.Length - 1;

        IWeapon weapon = Weapons[_selectedWeaponIndex].GetComponent<IWeapon>();
        if (weapon.Reloading || weapon.Shooting)
        {
            weapon.Reloading = false;
            weapon.Shooting = false;
        }

        Weapons[_selectedWeaponIndex].SetActive(false);
        _selectedWeaponIndex = newIndex;
        Weapons[_selectedWeaponIndex].SetActive(true);
        EquipWeapon();

        _ammoText.text = $"{_currentWeapon.CurrentAmmoInClip}/{_currentWeapon.CurrentAmmo - _currentWeapon.CurrentAmmoInClip}";
    }

    private IEnumerator ReloadAnimation(float reloadTime)
    {
        _fadeInTime = 0.2f;
        _fadeOutTime = 0.2f;

        float alpha = 0.0f;
        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / _fadeInTime;
            _reload.color = new Color(1f, 0f, 0.9848051f, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(reloadTime);

        alpha = 1.0f;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime / _fadeOutTime;
            _reload.color = new Color(1f, 0f, 0.9848051f, alpha);
            yield return null;
        }
    }
}

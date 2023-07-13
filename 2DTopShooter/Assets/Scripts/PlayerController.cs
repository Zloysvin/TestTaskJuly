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

    [SerializeField] private float _speed;
    [SerializeField] private Image _ammoType;
    [SerializeField] private TMP_Text _ammoText;

    public GameObject[] Weapons;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        EquipWeapon();

        foreach (var weapon in Weapons)
        {
            weapon.GetComponent<IWeapon>().OnShoot += PlayerController_OnShoot;
        }

        _ammoText.text = $"{_currentWeapon.CurrentAmmoInClip}/{_currentWeapon.CurrentAmmo - _currentWeapon.CurrentAmmoInClip}";
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
            _currentWeapon.Reload();

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
    }
}

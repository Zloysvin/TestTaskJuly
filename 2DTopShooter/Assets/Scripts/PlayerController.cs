using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public bool _automatic;

    private Rigidbody2D _rb;
    private Camera _camera;
    private IWeapon _currentWeapon;

    private Vector2 _moveDirection;

    [SerializeField] private float _speed;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        EquipWeapon();
    }

    void Update()
    {
        LookAtMouse();

        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");
        if (!_automatic)
        {
            if (Input.GetMouseButtonDown(0))
                _currentWeapon.Shoot();
        }
        else
            if (Input.GetMouseButton(0))
                _currentWeapon.Shoot();

        if (Input.GetMouseButton(1))
            _currentWeapon.Reload();
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
        Debug.Log(_currentWeapon.Name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float Damage;

    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);

        Destroy(this.gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamagable damageModel;
        if (collision.gameObject.TryGetComponent(out damageModel))
        {
            damageModel.ReceiveDamage(Damage);
        }
        Destroy(gameObject);
    }
}

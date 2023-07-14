using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float HP { get; set; }
    [field: SerializeField] public float HPMax { get; set; }
    public event EventHandler<DamageTakenEventArgs> OnDamageTaken;
    public event EventHandler<DeathEventArgs> OnDeath;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay;

    private Transform _target;
    private NavMeshAgent _agent;
    private bool _dealingDamage;

    void Awake()
    {
        HP = HPMax;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _target = GameObject.FindWithTag("Player").transform;
        _target.GetComponent<PlayerController>().OnDeath += Player_OnDeath;
    }

    private void Player_OnDeath(object sender, DeathEventArgs e)
    {
        _target = transform;
    }

    void Update()
    {
        _agent.SetDestination(new Vector3(_target.position.x, _target.position.y, transform.position.z));
    }
    public void ReceiveDamage(float damage, bool isShootingPlayer)
    {
        HP -= damage;
        Debug.Log(HP);

        if (HP <= 0)
        {
            OnDeath(this, new DeathEventArgs(isShootingPlayer));
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!_dealingDamage && other.tag == "Player")
        {
            StartCoroutine(DealMeleeDamage(other.GetComponent<IDamagable>()));
        }
    }

    private IEnumerator DealMeleeDamage(IDamagable target)
    {
        _dealingDamage = true;
        target.ReceiveDamage(_damage, false);
        yield return new WaitForSeconds(_attackDelay);
        _dealingDamage = false;
    }
}
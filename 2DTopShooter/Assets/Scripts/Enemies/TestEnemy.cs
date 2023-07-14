using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float HP { get; set; }
    [field: SerializeField] public float HPMax { get; set; }

    private Transform _target;
    private NavMeshAgent _agent;

    void Awake()
    {
        HP = HPMax;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        _agent.SetDestination(new Vector3(_target.position.x, _target.position.y, transform.position.z));
    }
    public void ReceiveDamage(float damage)
    {
        HP -= damage;
        Debug.Log(HP);

        if (HP <= 0)
            Destroy(gameObject);
    }
}
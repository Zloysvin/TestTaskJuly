using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float HP { get; set; }
    [field: SerializeField] public float HPMax { get; set; }

    private Transform _target;
    private NavMeshAgent _agent;
    private IWeapon _weapon;
    private Transform _weaponGunPoint;

    void Awake()
    {
        HP = HPMax;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _target = GameObject.FindWithTag("Player").transform;

        _weapon = GetComponentInChildren<IWeapon>();
        _weaponGunPoint = _weapon.GunPoint;
    }

    void Update()
    {
        //Vector3 direction = transform.position - new Vector3(transform.position.x, transform.position.y);
        transform.up = new Vector3(_agent.velocity.x, _agent.velocity.y, 0f);
        //Debug.DrawRay(transform.position, new Vector3(_agent.velocity.x, _agent.velocity.y, 0f));
        _agent.SetDestination(new Vector3(_target.position.x, _target.position.y, transform.position.z));
        RaycastHit2D hit = Physics2D.Raycast(_weaponGunPoint.position,_weaponGunPoint.up, 20f);
            
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.tag == "Player")
            {
                _weapon.Shoot();
                Debug.DrawRay(_weaponGunPoint.position, _weaponGunPoint.up * 20f, Color.green);
            }
            else
            {
                Debug.DrawRay(_weaponGunPoint.position, _weaponGunPoint.up * 20f, Color.red);
            }
        }
    }

    void LateUpdate()
    {
        //transform.rotation = Quaternion.LookRotation(new Vector3(_agent.velocity.x, _agent.velocity.y, 0f));
    }
    public void ReceiveDamage(float damage)
    {
        HP -= damage;
        Debug.Log(HP);

        if (HP <= 0)
            Destroy(gameObject);
    }
}

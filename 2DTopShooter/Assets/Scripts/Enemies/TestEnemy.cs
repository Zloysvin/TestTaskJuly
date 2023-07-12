using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] public float HP { get; set; }
    [field: SerializeField] public float HPMax { get; set; }

    void Awake()
    {
        HP = HPMax;
    }
    public void ReceiveDamage(float damage)
    {
        HP -= damage;
        Debug.Log(HP);

        if(HP <= 0)
            Destroy(gameObject);
    }
}

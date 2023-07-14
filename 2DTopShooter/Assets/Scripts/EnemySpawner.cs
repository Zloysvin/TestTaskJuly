using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float spawnRateMin = 2.0f;
    public float spawnRateMax = 5.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;

    void Start()
    {
        Invoke(nameof(Spawn), 0f);
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawndirection = Random.insideUnitCircle.normalized * spawnDistance;
            Vector3 spawnpoint = transform.position + spawndirection;

            NavMeshHit hit;
            for (int j = 0; j < 100; j++)
            {
                if (NavMesh.SamplePosition(spawnpoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    Debug.Log(hit.hit);
                    Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnpoint, Quaternion.identity);
                    break;
                }
            }
        }
        Invoke(nameof(Spawn), Random.Range(spawnRateMin, spawnRateMax));
    }
}

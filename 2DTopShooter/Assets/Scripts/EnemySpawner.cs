using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float spawnRateMin = 2.0f;
    public float spawnRateMax = 5.0f;
    public int spawnAmount = 1;
    public float spawnDistance = 15.0f;

    [SerializeField] private TMP_Text _killCountText;

    public int killCount;

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
                    GameObject enemy = Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnpoint, Quaternion.identity);
                    enemy.GetComponent<IDamagable>().OnDeath += EnemySpawner_OnDeath;
                    break;
                }
            }
        }
        Invoke(nameof(Spawn), Random.Range(spawnRateMin, spawnRateMax));
    }

    private void EnemySpawner_OnDeath(object sender, DeathEventArgs e)
    {
        if (e.KilledByPlayer)
        {
            killCount++;
            _killCountText.text = $"{killCount} X";
        }
    }
}

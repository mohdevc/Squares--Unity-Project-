using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemyPrefab;
    public Transform[] _spawnPoints;

    [SerializeField] float _respawnInterval = 5.0f;

    void Awake()
    {
        gameObject.GetComponent<EnemySpawner>().enabled = false;
    }
    public void Respawning()
    {
        gameObject.GetComponent<EnemySpawner>().enabled = true;
        InvokeRepeating("RespawnEnemy", _respawnInterval, _respawnInterval);
    }

    public void RespawnEnemy()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[index];

        Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}

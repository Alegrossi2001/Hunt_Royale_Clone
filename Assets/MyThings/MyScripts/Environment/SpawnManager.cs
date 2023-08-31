using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private SpawnPoint[] spawnPoints;

    private void Awake()
    {
        this.spawnPoints = GetComponentsInChildren<SpawnPoint>();

        spawnPoints.ShuffleIndices();

        int numberOfSpawnsAtStart = this.spawnPoints.Length - Mathf.FloorToInt(this.spawnPoints.Length * 0.2f); ;

        for (int i = 0; i < numberOfSpawnsAtStart; i++)
        {
            //occupies the position
            spawnPoints[i].SetSpawnPointOccupied();
            spawnPoints[i].Spawn();
        }
    }

    private void Start()
    {
        Enemy.OnDeath += SpawnNewEnemy;
    }

    private void SpawnNewEnemy(object sender, System.EventArgs e)
    {
        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

        }
        while (spawnPoints[randomIndex].IsSpawnPointOccupied());

        spawnPoints[randomIndex].Spawn();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool isSpawnPointOccupied;

    public void Spawn()
    {
        Transform enemyTransform = Resources.Load<Transform>("Enemy");
        Transform enemy = Instantiate(enemyTransform, this.transform.position, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetSpawnPoint(this);
    }

    public bool IsSpawnPointOccupied()
    {
        return isSpawnPointOccupied;
    }

    public void SetSpawnPointOccupied()
    {
        isSpawnPointOccupied = true;
    }

    public void RemoveSpawnPoint()
    {
        isSpawnPointOccupied = false;
    }


}

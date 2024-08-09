using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void SpawnNewEnemy()
    {
        Instantiate(enemyPrefab, new Vector2(Random.Range(-20f, 40f), Random.Range(-15f, 30f)), Quaternion.identity);
    }
}
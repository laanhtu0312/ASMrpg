using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class enemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int maxEnemies = 3;

    private int enemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemyCount < maxEnemies)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemyCount++;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public void EnemyDied()
    {
        enemyCount--;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
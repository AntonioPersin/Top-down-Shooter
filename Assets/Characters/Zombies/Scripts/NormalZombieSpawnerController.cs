using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float spawnInterval = 5f;

	private float timeSinceLastSpawn = 0f;

	void Update()
	{
		// Spawn enemies periodically
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= spawnInterval)
		{
			SpawnEnemy();
			timeSinceLastSpawn = 0f;
		}
	}

	void SpawnEnemy()
	{
		// Instantiate an enemy at the spawner's position
		Instantiate(enemyPrefab, transform.position, Quaternion.identity);
	}
}

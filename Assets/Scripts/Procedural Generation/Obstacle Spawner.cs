using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	// Params
	[SerializeField] GameObject[] obstaclePrefabs;
	[SerializeField] Transform obstacleParent;
	[SerializeField] float obstacleSpawnTime = 1;
	[SerializeField] float minObstacleSpawnTime = 0.1f;
	[SerializeField] float spawnWidth = 4;

	// State
	int obstacleSpawned = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
			StartCoroutine(SpawnObstacleRoutine());
    }


	IEnumerator SpawnObstacleRoutine ()
	{
		while (true)
		{
			GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
			Vector3 spawnPosition = obstacleParent.transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
			yield return new WaitForSeconds(obstacleSpawnTime);
			Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
			obstacleSpawned++;
		}
	}


	public void DecreaseSpawnTime (float amount)
	{
		obstacleSpawnTime -= amount;
		obstacleSpawnTime = Mathf.Max(obstacleSpawnTime, minObstacleSpawnTime);
	}
}

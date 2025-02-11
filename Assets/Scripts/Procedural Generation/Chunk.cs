using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	// Params
	[SerializeField] GameObject fencePrefab;
	[SerializeField] GameObject applePrefab;
	[SerializeField] GameObject coinPrefab;
	[SerializeField] float[] lanes = {-2.5f, 0, 2.5f};
	[SerializeField] float appleSpawnChance = 0.3f;
	[SerializeField] float coinSpawnChance = 0.5f;

	// Cache
	LevelGenerator levelGenerator;
	ScoreManager scoreManager;

	// State
	readonly List<int> availableLanes = new List<int> {0, 1, 2};



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFences();
		SpawnApple();
		SpawnCoins();
    }


	public void Init (LevelGenerator levelGenerator, ScoreManager scoreManager)
	{
		this.levelGenerator = levelGenerator;
		this.scoreManager = scoreManager;
	}


	int SelectLane ()
	{
		int randomLaneIndex = Random.Range(0, availableLanes.Count);
		int selectedLane = availableLanes[randomLaneIndex];
		availableLanes.RemoveAt(randomLaneIndex);
		return selectedLane;
	}


	void SpawnFences ()
	{
		int fencesToSpawn = Random.Range(0, lanes.Length);

		for (int i = 0; i < fencesToSpawn; i++)
		{
			if (availableLanes.Count <= 0) break;
			Instantiate(fencePrefab, FindSpawnPosition(), Quaternion.identity, this.transform);
		}
	}


	private Vector3 FindSpawnPosition ()
	{
		return new Vector3(lanes[SelectLane()], transform.position.y, transform.position.z);
	}


	void SpawnApple ()
	{
		if (Random.value > appleSpawnChance) return;
		if (availableLanes.Count <= 0) return;

		GameObject newAppleGO = Instantiate(applePrefab, FindSpawnPosition(), Quaternion.identity, this.transform);
		Apple newApple = newAppleGO.GetComponent<Apple>();
		newApple.Init(levelGenerator);
	}


	void SpawnCoins ()
	{
		if (Random.value > coinSpawnChance) return;
		if (availableLanes.Count <= 0) return;

		int coinsToSpawn = Random.Range(1, 6); // 1 to 5 inclusive;
		Vector3 spawnPosition = FindSpawnPosition();

		for (int i = 0; i < coinsToSpawn; i++)
		{
			GameObject newCoinGO = Instantiate(coinPrefab, spawnPosition - (2 * (i-2) * transform.forward), Quaternion.identity, this.transform);
			Coin newCoin = newCoinGO.GetComponent<Coin>();
			newCoin.Init(scoreManager);
		}
	}
}

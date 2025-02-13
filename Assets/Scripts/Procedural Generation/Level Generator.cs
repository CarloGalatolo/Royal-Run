using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	// Params
	[Header("References")]
	[SerializeField] GameObject[] chunkPrefabs;
	[SerializeField] GameObject checkpointChunkPrefab;
	[SerializeField] Transform chunkParent;
	[SerializeField] CameraController cameraController;
	[SerializeField] GameManager gameManager;
	[SerializeField] ScoreManager scoreManager;

	[Header("Level Settings")]
	[SerializeField] int startingChunksAmount = 12;
	[Tooltip("Do not chunk length value unless chink prefab size reflects that change.")]
	[SerializeField] float chunkLength = 10;
	[SerializeField] float moveSpeed = 8;
	[SerializeField] float minSpeed = 2;
	[SerializeField] float maxSpeed = 20;
	[SerializeField] uint checkpointInterval = 8;

	// State
	readonly List<GameObject> chunks = new List<GameObject>();
	uint chunksSpawned = 0;
	


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start ()
    {
		// chunks = new GameObject[startingChunksAmount];	// Parametric array definition.

		// Spawn Chunks \\
		for (int i = 0; i < startingChunksAmount; i++)
		{
			SpawnChunk();
		}
    }


    // Update is called once per frame
    void Update ()
    {
        MoveChunks();
    }


	GameObject ChooseChunkToSpawn ()
	{
		if (chunksSpawned % checkpointInterval == 0)
		{
			return checkpointChunkPrefab;
		}
		else
		{
			return chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
		}
	}


	Vector3 FindChunkSpawnPosition ()
	{
		if ( chunks.Count == 0 )
		{
			return new Vector3(0, 0, transform.position.z);
		}
		else
		{
			return new Vector3(0, 0, chunks[^1].transform.position.z + chunkLength);	// Last element of the list, same as chunks.Count - 1.
		}
		// return new Vector3(0, 0, chunks.Count == 0 ? transform.position.z : chunks[^1].transform.position.z + chunkLength );	// Shorter but less readable version.
	}


	void MoveChunks ()
	{
		for (int i = 0; i < chunks.Count; i++)	// Using foreach gives an InvalidOperationExeption at the moment when the chunk is destroyed and the function is called again. Works correctly anyway.
		{
			GameObject chunk = chunks[i];
			chunk.transform.Translate(moveSpeed * Time.deltaTime * -transform.forward);

			// Replace the chunk if ended up behind the camera.
			if ( chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength )
			{
				// Destroy previous chunk.
				chunks.Remove(chunk);
				Destroy(chunk);
				
				// Add another chunk at the end of the row.
				SpawnChunk();
			}
		}
	}


	/// <summary>
	/// This should only apply the change if the new amount is bound within range.
	/// I.e. if the old value is an edge of the range, the change should not occur and the value should be reverted.
	/// </summary>
	/// <param name="amount"></param>
	public void ChangeChunkSpeed (float amount)
	{
		moveSpeed += amount;

		if (moveSpeed > minSpeed && moveSpeed < maxSpeed)	// Check it's in range.
		{
			Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - amount * 0.8f);
			cameraController.ChangeFOV(amount);
		}

		moveSpeed = Mathf.Clamp(moveSpeed, minSpeed, maxSpeed);	// If not in range, clamp to range.
		// Debug.Log($"New moveSpeed = {moveSpeed}");
		// Debug.Log($"New gravity = {Physics.gravity.z}");
	}


	void SpawnChunk ()
	{
		chunksSpawned++;
	
		GameObject newChunk = Instantiate(ChooseChunkToSpawn(), FindChunkSpawnPosition(), Quaternion.identity, chunkParent);
		chunks.Add(newChunk);
		newChunk.GetComponent<Chunk>().Init(this, scoreManager);
	}
}

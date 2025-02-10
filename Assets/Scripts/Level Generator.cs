using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	// Params
	[SerializeField] GameObject chunkPrefab;
	[SerializeField] Transform chunkParent;
	[SerializeField] int startingChunksAmount = 12;
	[SerializeField] float chunkLength = 10;
	[SerializeField] float moveSpeed = 8;

	// State
	readonly List<GameObject> chunks = new List<GameObject>();
	


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


	void SpawnChunk ()
	{
		chunks.Add( Instantiate(chunkPrefab, FindChunkSpawnPosition(), Quaternion.identity, chunkParent) );
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
}

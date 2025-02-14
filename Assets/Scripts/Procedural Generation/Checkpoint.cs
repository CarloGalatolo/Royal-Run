using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	// Params
	[SerializeField] TMP_Text signText;
	[SerializeField] float bonusTime = 30;
	[SerializeField] float obstacleDecreaseTimeAmount = 0.1f;

	// Cache
	GameManager gameManager;
	ObstacleSpawner obstacleSpawner;

	// Consts
	const string PLAYER_TAG = "Player";



	void Start()
	{
		gameManager = FindFirstObjectByType<GameManager>();
		obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
		signText.text = "+" + bonusTime;
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag(PLAYER_TAG))
		{
			gameManager.AddTime(bonusTime);
			obstacleSpawner.DecreaseSpawnTime(obstacleDecreaseTimeAmount);
		}
	}
}

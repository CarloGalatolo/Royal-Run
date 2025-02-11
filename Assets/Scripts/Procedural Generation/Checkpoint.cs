using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	// Params
	[SerializeField] TMP_Text signText;
	[SerializeField] float bonusTime = 30;

	// Cache
	GameManager gameManager;

	// Consts
	const string PLAYER_TAG = "Player";



	void Start()
	{
		gameManager = FindFirstObjectByType<GameManager>();
		signText.text = "+" + bonusTime;
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag(PLAYER_TAG))
		{
			gameManager.AddTime(bonusTime);
		}
	}
}

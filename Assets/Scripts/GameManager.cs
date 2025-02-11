using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Params
	[SerializeField] PlayerController playerController;
	[SerializeField] TMP_Text timeText;
	[SerializeField] GameObject gameOverText;
	[SerializeField] string defaultText = "Castle crumbles in";
	[SerializeField] float startTime = 30;

	// State
	float timeLeft;

	// Properties
	public bool GameIsOver { get; private set; } = false;



	void Start ()
	{
		timeLeft = startTime;
	}


	void Update ()
	{
		Countdown();
	}


	void Countdown ()
	{
		if (GameIsOver) return;
		
		timeLeft -= Time.deltaTime;
		timeText.text = defaultText + " " + timeLeft.ToString("F1");

		if (timeLeft <= 0)
		{
			GameOver();
		}
	}


	void GameOver ()
	{
		GameIsOver = true;
		gameOverText.SetActive(true);
		playerController.enabled = false;
		Time.timeScale = 0.1f;
	}


	public void AddTime (float amount)
	{
		timeLeft += amount;
	}
}

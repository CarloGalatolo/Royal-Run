using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	// Params
	[SerializeField] GameManager gameManager;
	[SerializeField] TMP_Text scoreText;
	[SerializeField] string defaultText = "Filthy wealth";

	// State
	uint score = 0;



	public void IncreaseScore (uint amount)
	{
		if ( gameManager.GameIsOver ) return;

		score += amount;
		scoreText.text = defaultText + ": " + score;
	}
}

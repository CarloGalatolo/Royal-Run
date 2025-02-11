using UnityEngine;

public class Coin : Pickup
{
	// Params
	[SerializeField] uint scoreAmount = 1;
	
	// Cache
	ScoreManager scoreManager;



	public void Init (ScoreManager scoreManager)
	{
		this.scoreManager = scoreManager;
	}


	protected override void OnPickup()
	{
		// Debug.Log("Picking up Coin");
		scoreManager.IncreaseScore(scoreAmount);
	}
}

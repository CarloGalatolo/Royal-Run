using UnityEngine;

public class Apple : Pickup
{
	// Params
	[SerializeField] float speedChangeAmount = 2;

	// Cache
	LevelGenerator levelGenerator;



	public void Init (LevelGenerator levelGenerator)
	{
		this.levelGenerator = levelGenerator;
	}


	protected override void OnPickup()
	{
		// Debug.Log("Picking up Apple");
		levelGenerator.ChangeChunkSpeed(speedChangeAmount);
	}
}

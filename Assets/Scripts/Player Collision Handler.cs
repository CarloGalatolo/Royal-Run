using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
	// Params
	[SerializeField] Animator animator;
	[SerializeField] float hitCooldown = 1;
	[SerializeField] float speedChangeAmount = -2;

	// Cache
	LevelGenerator levelGenerator;

	// Consts
	const string HIT_TRIGGER = "Hit";

	// State
	float hitTimer = 0;


	void Start ()
	{
		levelGenerator = FindFirstObjectByType<LevelGenerator>();
	}


	void Update ()
	{
		hitTimer += Time.deltaTime;
	}


	void OnCollisionEnter (Collision other)
	{
		if (hitTimer < hitCooldown) return;
		
		levelGenerator.ChangeChunkSpeed(speedChangeAmount);
		animator.SetTrigger(HIT_TRIGGER);
		hitTimer = 0;	// Reset timer.
	}
}

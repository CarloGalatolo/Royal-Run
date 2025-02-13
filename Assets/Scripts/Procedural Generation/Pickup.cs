using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
	// Params
	[SerializeField] float rotationSpeed = 100;

	// Consts
	const string PLAYER_TAG = "Player";


	// Abstracts
	protected abstract void OnPickup();


	void Update ()
	{
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag(PLAYER_TAG))
		{
			// Debug.Log("Picking up");
			OnPickup();
			Destroy(this.gameObject);
		}
		else
		{
			// Debug tag comparison.
			// Debug.Log(other.gameObject.name + " triggering this is not Player.");
		}
	}
}

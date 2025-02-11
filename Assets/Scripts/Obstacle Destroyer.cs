using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		Destroy(other.gameObject);
	}
}

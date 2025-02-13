using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
	[SerializeField] float shakeGain = 2;
	CinemachineImpulseSource cinemachineImpulseSource;

	void Awake ()
	{
		cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
	}


	void OnCollisionEnter (Collision collision)
	{
		float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
		float shake = 1 / distance * shakeGain;
		shake = Mathf.Min (shake, 1);
		cinemachineImpulseSource.GenerateImpulse(shake);
	}
}

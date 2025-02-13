using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
	// Params
	[SerializeField] ParticleSystem collisionParticleSystem;
	[SerializeField] AudioSource boulderSmashAudioSource;
	[SerializeField] float shakeGain = 2;
	[SerializeField] float collisionCooldown = 1;

	// Cache
	CinemachineImpulseSource cinemachineImpulseSource;
	
	// State
	float collisionTimer = 1;

	void Awake ()
	{
		cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
	}


	void Update ()
	{
		collisionTimer += Time.deltaTime;
	}


	void OnCollisionEnter (Collision collision)
	{
		if (collisionTimer >= collisionCooldown)
		{
			FireCameraShake();
			PlayEffects(collision);
			collisionTimer = 0;
		}
	}


	void FireCameraShake()
	{
		float distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
		float shake = 1 / distance * shakeGain;
		shake = Mathf.Min(shake, 1);
		cinemachineImpulseSource.GenerateImpulse(shake);
	}


	void PlayEffects(Collision collision)
	{
		ContactPoint contactPoint = collision.contacts[0];
		collisionParticleSystem.transform.position = contactPoint.point;
		collisionParticleSystem.Play();
		boulderSmashAudioSource.Play();
	}
}

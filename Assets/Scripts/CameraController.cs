using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Params
	[SerializeField] ParticleSystem speedupParticleSystem;
	[SerializeField] float minFOV = 20;
	[SerializeField] float maxFOV = 120;
	[SerializeField] float zoomDuration = 1;
	[SerializeField] float zoomModifier = 5;

	// Cache
	CinemachineCamera cinemachineCamera;


	void Awake ()
	{
		// Tip: get references to own components in Awake, while to others' components in Start.
		cinemachineCamera = GetComponent<CinemachineCamera>();
	}

	public void ChangeFOV (float amount)
	{
		StopAllCoroutines();
		StartCoroutine( ChangeFOVRoutine(amount) );

		if (amount > 0)
		{
			speedupParticleSystem.Play();
		}

		// Debug.Log($"New FOV = {cinemachineCamera.Lens.FieldOfView}");
	}


	IEnumerator ChangeFOVRoutine (float amount)
	{
		float startFOV = cinemachineCamera.Lens.FieldOfView;
		float targetFOV = Mathf.Clamp(startFOV + amount * zoomModifier, minFOV, maxFOV);

		float elapsedTime = 0;

		while (elapsedTime < zoomDuration)
		{
			float t = elapsedTime / zoomDuration;
			elapsedTime += Time.deltaTime;

			cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

			yield return null;	// Delay until next tick.
		}

		cinemachineCamera.Lens.FieldOfView = targetFOV;	// Safety measure, in case Lerp fails.
	}
}

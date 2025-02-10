using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	// Params
	[SerializeField] float moveSpeed = 1;
	[SerializeField] float clampX = 4;
	[SerializeField] float clampZ = 4;

	
	// Cache
	Rigidbody rigidBody;

	// State
	Vector2 movement;


	void Awake ()
	{
        rigidBody = GetComponent<Rigidbody>();
	}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	void FixedUpdate ()
	{
		Vector3 translation = new( Mathf.Clamp(this.rigidBody.position.x + movement.x, -clampX, clampX),
								   this.rigidBody.position.y,
								   Mathf.Clamp(this.rigidBody.position.z + movement.y, -clampZ, clampZ)
		);
		rigidBody.MovePosition(translation);
	}


	public void Move (InputAction.CallbackContext context)
	{
		movement = moveSpeed * Time.fixedDeltaTime * context.ReadValue<Vector2>();
	}
}

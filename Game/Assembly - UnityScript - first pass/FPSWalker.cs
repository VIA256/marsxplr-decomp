using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(CharacterController))]
public class FPSWalker : MonoBehaviour
{
	public float speed = 6.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private bool grounded = false;

	public void FixedUpdate()
	{
		if (grounded)
		{
            //We are grounded, so recalculate movedirection directly from axes
			moveDirection = new Vector3(
                Input.GetAxis("Horizontal"),
                0f,
                Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			if (Input.GetButton("Jump"))
			{
				moveDirection.y = jumpSpeed;
			}
		}

        // Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
		CharacterController characterController = (CharacterController)GetComponent(typeof(CharacterController));
		CollisionFlags flags = characterController.Move(moveDirection * Time.deltaTime);
        grounded = (flags & CollisionFlags.CollidedBelow) != 0;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

CharacterController controller;
float verticalVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {


        if (Keyboard.current == null)
        {
            return;
        }

        Vector3 movement = Vector3.zero;

        if (Keyboard.current.dKey.isPressed)
        {
            movement.x += 1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            movement.x -= 1f;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            movement.z += 1f;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            movement.z -= 1f;
        }

        movement = movement.normalized * moveSpeed;

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        verticalVelocity += gravity * Time.deltaTime;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);

    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (!hit.collider.CompareTag("Pushable"))
        {
            return;
        }

        Vector3 pushDirection = new Vector3(
            hit.moveDirection.x,
            0f,
            hit.moveDirection.z
        );

        body.linearVelocity = pushDirection * moveSpeed;
    }
}


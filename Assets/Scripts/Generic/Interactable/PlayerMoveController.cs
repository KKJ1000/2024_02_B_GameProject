using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private CharacterController controller;
    private Vector3 moveDirection;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            transform.forward = moveDirection;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}

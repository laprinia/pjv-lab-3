using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public CharacterController controller;
  public float movementSpeed=10f;
  public float gravity = -9.7f;
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;
  
  private Vector3 velocity;
  private bool isGrounded;
  
  
  private void Update()
  {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0)
    {
      velocity.y = -1.5f;
    }
    float xOffset = Input.GetAxis("Horizontal");
    float zOffset = Input.GetAxis("Vertical");
    Vector3 moveVector = transform.right * xOffset + transform.forward * zOffset;
    controller.Move(moveVector * movementSpeed * Time.deltaTime);

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity* Time.deltaTime);
  }
}

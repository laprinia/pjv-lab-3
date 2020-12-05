---
title: PJV LAB 3
tags: []
---

# PJV LAB 3

### Character Controller

- ori prin CharacterController ori RigidBody


1. Folosind SimpleMove(Vector3 speed) 
Tine cont de gravitiatea definita dar axa Y ignorata

```c#
// Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
```

```c#
//obtine valori intre -1 si 1 pentru o anumita axa 1 ar fi la joystick impins all the way
//frame independet
Input.GetAxis()
``` 

2. Folosind Move(Vector3 motion) 
> acceleratii de genul gravitatiei vor trebui implementate manual.
````c#
   moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
 
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
 
        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
 
        // Move the controller
````
### Rigidbody

o componenta ce determina ca obiectul este un corp fizic ce actioneaza cu mediul sau asupra caruia se aplica forte.
> implicit afectata de gravitate 
> 4 moduri de aplic a fortei: force, acceleration, impulse si velocityChange

````c#
rb.AddForce(10.0f * Vector3.up);
if (Input.GetButtonDown("Jump") && _isGrounded)
{
  rb.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
}
````
### Miscarea unui caracter

- de regula se foloseste MovePosition, cand ne folos de input

````c#
Vector3 dirVector = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")).normalized;
GetComponent <Rigidbody> ().MovePosition (transform.position + dirVector * Time.deltaTime);
````
> Functia de FixedUpdate care se apeleaza la un framerate fix ideal pt fizica.
## Inputul trb. normalizat
````c#
 Vector2 dirVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")).normalized;
````


### Rotatia camerei

- deobicei se tine rotatia curenta pt a evita transf compuse.
````c#
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update () {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
````
#### yaw si pitch??


### Raycasting
- trasare raza sursa destinatie
#### bun si pentru object picking

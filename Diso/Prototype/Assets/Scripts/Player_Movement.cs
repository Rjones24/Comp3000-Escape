using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController characterController;
    public float Speed = 2.0f;

    public Transform playerBody;
    public float gravity = 9.81f;
    Vector3 velocity;
    public float JumpHight = 3.0f;

    public Transform feet;
    public float DistanceToGround = 0.4f;
    public LayerMask groundMask;
    bool Grounded;


    // Update is called once per frame
    void Update()
    {

        Grounded = Physics.CheckSphere(feet.position, DistanceToGround, groundMask);
 
        if(Grounded && velocity.y < 0)
        {
            velocity.y = -1.0f;
        }

        float horizontal = Input.GetAxis("Horizontal") * Speed;
        float vertical = Input.GetAxis("Vertical") * Speed;
        Vector3 move =  transform.forward * vertical;
        characterController.Move(move * Time.deltaTime);
        playerBody.Rotate(Vector3.up * horizontal /6 );

        if(Input.GetButtonDown("Jump") && Grounded)
        {
            velocity.y = Mathf.Sqrt(JumpHight * 2.0f * gravity);
        }
      
        velocity.y += -gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}

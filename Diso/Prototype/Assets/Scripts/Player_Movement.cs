using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Movement : MonoBehaviourPunCallbacks 
{
    public CharacterController characterController;
    public float Speed = 2.0f;


    //camera
    public GameObject cameraparent;

    public Transform playerBody;
    public float gravity = 9.81f;
    Vector3 velocity;
    public float JumpHight = 3.0f;

    public Transform feet;
    public float DistanceToGround = 0.4f;
    public LayerMask groundMask;
    bool Grounded;
    private float horizontal;
    private float vertical;


    private void Start()
    {
            cameraparent.SetActive(photonView.IsMine);
    }

    // Update is called once per frame
    void Update()
    {

        if (this.photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        Grounded = Physics.CheckSphere(feet.position, DistanceToGround, groundMask);
 
        if(Grounded && velocity.y < 0)
        {
            velocity.y = -1.0f;
        }

        if (this.photonView.IsMine)
        {
            this.horizontal = Input.GetAxis("Horizontal") * Speed;
            this.vertical = Input.GetAxis("Vertical") * Speed;
            Vector3 move = transform.forward * vertical;
            this.characterController.Move(move * Time.deltaTime);
            this.playerBody.Rotate(Vector3.up * horizontal / 6);

            if (Input.GetButtonDown("Jump") && Grounded)
            {
                velocity.y = Mathf.Sqrt(JumpHight * 2.0f * gravity);
            }
        }
      
        velocity.y += -gravity * Time.deltaTime;
        this.characterController.Move(velocity * Time.deltaTime);
    }
}

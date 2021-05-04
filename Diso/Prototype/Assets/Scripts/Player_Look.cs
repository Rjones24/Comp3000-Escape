using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviourPunCallbacks
{
    public float MouseSpeed = 50.0f;

    public Transform playerBody;
    public Transform playerHead;
    private float XRotation = 0.0f;
    private float YRotation = 0.0f;

    private float mouseX , mouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (this.photonView.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;

            this.XRotation -= mouseY;
            this.XRotation = Mathf.Clamp(XRotation, -135.0f, -15.0f );

            this.YRotation -= mouseX;

            this.playerBody.transform.localRotation = Quaternion.Euler(0.0f , -YRotation, 0.0f);
            this.playerHead.transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        }
    }
}

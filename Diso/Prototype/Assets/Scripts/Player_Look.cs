using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Look : MonoBehaviour
{
    public float MouseSpeed = 100.0f;

    public Transform playerBody;
    public Transform playerHead;
    public float XRotation = 0.0f;
    public float YRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -45.0f, 45.0f);

        YRotation -= mouseX;
        YRotation = Mathf.Clamp(YRotation, -10.0f, 10.0f);

        playerHead.transform.localRotation = Quaternion.Euler(XRotation, -YRotation, 0.0f);

       
    }
}

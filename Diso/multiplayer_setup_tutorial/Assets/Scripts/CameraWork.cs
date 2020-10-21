using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{

    #region Private Fields

    [Tooltip("the distance to the target")]
    [SerializeField]
    private float distance = 7.0f;

    [Tooltip("the hight of the camera")]
    [SerializeField]
    private float height = 3.0f;

    [Tooltip("camera offset from the target")]
    [SerializeField]
    private Vector3 centerOffset = Vector3.zero;

    [Tooltip("set this to false untill manualy calle to start following")]
    [SerializeField]
    private bool followOnStart = false;

    [Tooltip("smothing for the following camera")]
    [SerializeField]
    private float smoothSpeed = 0.125f;

    Transform cameraTransform;
    bool isFollowing;
    Vector3 cameraOffset = Vector3.zero;

    #endregion

    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        if (followOnStart)
        {
            OnStartFollowing();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        if (isFollowing)
        {
            Follow();
        }
    }
    #endregion

    #region Public Methods

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        Cut();
    }

    #endregion

    #region Private Mathods
    void Follow()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);

        cameraTransform.LookAt(this.transform.position + centerOffset);
    }

    void Cut()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;

        cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);

        cameraTransform.LookAt(this.transform.position + centerOffset);
    }


    #endregion
}

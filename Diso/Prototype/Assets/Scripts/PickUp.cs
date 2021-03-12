using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickUp : MonoBehaviourPunCallbacks
{

    Vector3 objectpos;
    float distance;

    public bool canHold = true;
    public GameObject item;
    public bool isHolding = false;

    private Hands target;

    bool firstPick = true;

    Transform targetTransform;
    Vector3 targetPosition;
    Quaternion targetRotation;

    public void PLayersPos(Hands _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
       
        targetTransform = this.target.GetComponent<Transform>();

    }

    public void firstPickup()
    {
        item.transform.SetParent(GameObject.Find("PhotonMono").GetComponent<Transform>(), false);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        

        if (isHolding == true)
        {
            if (firstPick)
            {
                firstPickup();
                firstPick = false;
            }
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().detectCollisions = true;

            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                this.transform.position = targetPosition;

               // targetRotation = targetTransform.rotation;
               // this.transform.rotation = targetRotation;
            }

        }
        else
        {
            objectpos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectpos;
        }
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void OnMouseDown()
    {
        isHolding = true;

    }
    void OnMouseUp()
    {
        isHolding = false;
        firstPick = true;
    }
}

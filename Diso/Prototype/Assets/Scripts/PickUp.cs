using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickUp : MonoBehaviourPunCallbacks
{

    Vector3 objectpos;

   public  float dist = 0.0f;

    public bool canHold = true;
    public GameObject item;
    public bool isHolding = false;

    private GameObject target;

    bool firstPick = true;

    public float DistanceToGround = 0.4f;
    public LayerMask groundMask;
    bool Grounded;

    Transform targetTransform;
    Vector3 targetPosition;

    public void PLayersPos(GameObject _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
       
        targetTransform = target.GetComponent<Transform>();

    }

    public void firstPickup()
    {
        item.transform.SetParent(GameObject.Find("PhotonMono").GetComponent<Transform>(), false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isHolding = false;
        firstPick = true;
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
        Grounded = Physics.CheckSphere(transform.position, DistanceToGround, groundMask);
        if (Grounded)
        {
            dist = 2.5f;
        }
        else
        {
            dist = 0.1f;
        }
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void OnMouseDown()
    {
        if (dist > Vector3.Distance(target.transform.position, transform.position))
        {
            isHolding = true;
            base.photonView.RequestOwnership();
        }

    }
    void OnMouseUp()
    {
        isHolding = false;
        firstPick = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Open_On_Click : MonoBehaviourPunCallbacks
{
    public float dist = 1.5f;

    public GameObject item ;
    public bool isInteracting = false;

    private GameObject target;
    Vector3 move = new Vector3(1.0f, 0.0f, 0.0f);

    bool firstPick = true;


    public void PLayersPos(GameObject _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
    }

   
    // Update is called once per frame
    void LateUpdate()
    {
        if (isInteracting == true)
        {
            if (firstPick)
            {
                this.transform.position += move;

                firstPick = false;
            }
            else
            {
                this.transform.position -= move;
                firstPick = true;
            }
            isInteracting = false;
        }     
    }


    void OnMouseDown()
    {
        if (dist > Vector3.Distance(target.transform.position, transform.position))
        {
            isInteracting = true;

            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }

    }
    void OnMouseUp()
    {
        isInteracting = false;
    }
}


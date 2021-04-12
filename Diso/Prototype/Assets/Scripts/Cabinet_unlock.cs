using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet_unlock : MonoBehaviourPunCallbacks
{
    public GameObject PopUp;

    public GameObject item;
    Vector3 move = new Vector3(-0.3f, 0.0f, 0.0f);


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PopUp.SetActive(true);
        }
        else if (other.gameObject.tag.Equals("key"))
        {
            item.transform.position += move;
            Destroy(this.gameObject);  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PopUp.SetActive(false);
    }
}

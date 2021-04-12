using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwipe_2 : MonoBehaviourPunCallbacks
{
    public GameObject PopUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PopUp.SetActive(true);
        }
        else if (other.gameObject.tag.Equals("Card"))
        {
            var OpenDoor = GameObject.FindWithTag("SpawnDoor");
            OpenDoor.SendMessage("Door2", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PopUp.SetActive(false);
    }

}


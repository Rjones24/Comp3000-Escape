using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSwipe_2 : MonoBehaviourPunCallbacks
{
    public GameObject PopUp;
    public string name;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PopUp.SetActive(true);
        }
        else if (other.gameObject.tag.Equals("Card"))
        {
            if (name == "swipe1")
            {
                var OpenDoor = GameObject.FindWithTag("SpawnDoor");
                OpenDoor.SendMessage("Door1", true);
            }
            else if (name == "swipe2")
            {
                var OpenDoor = GameObject.FindWithTag("SpawnDoor");
                OpenDoor.SendMessage("Door2", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PopUp.SetActive(false);
    }

}


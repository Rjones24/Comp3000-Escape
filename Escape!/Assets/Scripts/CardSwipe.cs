using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardSwipe : MonoBehaviourPunCallbacks
{
    public GameObject PopUp;
    public string spesifiedtag;

    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag.Equals("Player"))
    {
         PopUp.SetActive(true);
    } else if (other.gameObject.tag.Equals("Card"))
    {
            if (spesifiedtag == "swipe1")
            {
                var OpenDoor = GameObject.FindWithTag("SpawnDoor");
                OpenDoor.SendMessage("Door1", true);
            }
            else if (spesifiedtag == "swipe2")
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

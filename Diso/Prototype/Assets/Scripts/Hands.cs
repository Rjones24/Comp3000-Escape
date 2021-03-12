using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hands : MonoBehaviourPunCallbacks
{
    void Start()
    {
        var ItemPickup = GameObject.FindWithTag("Pickups");
        ItemPickup.SendMessage("PLayersPos", this, SendMessageOptions.RequireReceiver);
    }
}

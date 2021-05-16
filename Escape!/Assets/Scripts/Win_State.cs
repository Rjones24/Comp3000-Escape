using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class Win_State : MonoBehaviour 
{


    private void OnTriggerEnter(Collider other)
    {
        PhotonNetwork.Disconnect();
        var OpenDoor = GameObject.FindWithTag("SpawnDoor");
        OpenDoor.SendMessage("Door1", false);
        OpenDoor.SendMessage("Door2", false);
        StartCoroutine(DisconnectandLoad());
    }



    IEnumerator DisconnectandLoad()
    {
        while (PhotonNetwork.IsConnected)
        { yield return null; }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

}

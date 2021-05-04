using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Open_Door : MonoBehaviourPunCallbacks
{

    public GameObject door;
    public GameObject door2;

    public GameObject PopUp;

    static bool door1Open = false;
    static bool door2Open = false;

    public bool onTrigger;

    public void Door1(bool is1Open)
    {
        door1Open = is1Open;
    }

    public void Door2(bool is2Open)
    {
        door2Open = is2Open;
    }

    private void Update()
    {
        if (door1Open && door2Open)
        {
            door.transform.localRotation = Quaternion.Euler(0.0f, -90, 0.0f);

            door2.transform.localRotation = Quaternion.Euler(0.0f, -90, 0.0f);

            PopUp.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PopUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PopUp.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Door : MonoBehaviour
{

    public GameObject door;

    public Transform doorHinge;

    public bool doorOpen = false;

    public bool onTrigger;

    private void Update()
    {
        if (doorOpen)
        {
            door.transform.Translate(3.0f, 0f, 0f); ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    private void OnGUI()
    {
        if (!doorOpen)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(850, 700, 200, 25), "Press 'E' to Interact with Keypad");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    doorOpen = true;
                }
            }
        }
    }       
}

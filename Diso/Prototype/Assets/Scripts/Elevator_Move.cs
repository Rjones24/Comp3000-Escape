using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Move : MonoBehaviour
{
    public GameObject elevator;
    public GameObject door;

    static bool B1 = false;
    static bool B2 = false;
    static bool B3 = false;
    static bool B4 = false;

    public void Button1(bool iscorrect)
    {
        B1 = iscorrect;
    }

    public void Button2(bool iscorrect)
    {
        B2 = iscorrect;
    }
    public void Button3(bool iscorrect)
    {
        B3 = iscorrect;
    }
    public void Button4(bool iscorrect)
    {
        B4 = iscorrect;
    }


    // Update is called once per frame
    void Update()
    {
        if (B1 && B2 && B3 && B4)
        {
            elevator.transform.position = new Vector3(-9.0f,-12.5f,81.0f);
            door.transform.localRotation = Quaternion.Euler(0.0f, 90, 0.0f);
        }
    }
}

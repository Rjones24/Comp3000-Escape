using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_State : MonoBehaviour
{    private void OnTriggerEnter(Collider other)
    {
        GameManager.win = true;
    }
}

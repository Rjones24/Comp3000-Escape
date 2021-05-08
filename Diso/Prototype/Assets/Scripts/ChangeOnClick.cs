using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeOnClick : MonoBehaviour
{

   // public GameObject item;
    public bool isInteracting = false;

    public int color;
    public string spesifiedtag;

    // Update is called once per frame

    void Update()
    {
 
            CheckColor(color);


        if (spesifiedtag == "b1")
        {
            if(color == 4)
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button1", true);
            }
            else
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button1", false);
            }
        }
        else if (spesifiedtag == "b2")
        {
            if (color == 6)
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button2", true);
            }
            else
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button2", false);
            }
        }
        else if (spesifiedtag == "b3")
        {
            if (color == 1)
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button3", true);
            }
            else
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button3", false);
            }
        }
        else if (spesifiedtag == "b4")
        {
            if (color == 4)
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button4", true);
            }
            else
            {
                var checkbutton = GameObject.FindWithTag("Elea");
                checkbutton.SendMessage("Button4", false);
            }
        }
    }


    void OnMouseDown()
    {
        if (color > 9)
        {
            color = 0;
        }
        else
        {
            color += 1;
        }
    }

    void CheckColor(int i)
    {
        switch (i)
        {
            case 0:
                gameObject.GetComponent<Renderer>().material.color = new Color(100.0f, 0.0f, 100.0f);//purple
                break;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = new Color(255.0f, 0.0f, 255.0f);//orange
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = new Color(255.0f, 0.0f, 0.0f);//red
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 255.0f);//blue
                break;
            case 4:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 255.0f, 0.0f);//green
                break;
            case 5:
                gameObject.GetComponent<Renderer>().material.color = new Color(255.0f, 255.0f, 0.0f);//yellow
                break;
            case 6:
                gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f);//black
                break;
            case 7:
                gameObject.GetComponent<Renderer>().material.color = new Color(255.0f, 255.0f, 255.0f);//white
                break;
            case 8:
                gameObject.GetComponent<Renderer>().material.color = new Color(255.0f, 50.0f, 255.0f);//pink
                break;
            case 9:
                gameObject.GetComponent<Renderer>().material.color = new Color(102.0f, 50.0f, 0.0f);//brown
                break;
        }
    }
}

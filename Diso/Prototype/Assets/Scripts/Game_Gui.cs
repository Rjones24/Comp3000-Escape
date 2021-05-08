using UnityEngine;



public class Game_Gui : MonoBehaviour
{

    public bool GUI_WindowOn = true;

    /// <summary>Positioning rect for window.</summary>
    public Rect GuiPopup = new Rect(0, 150, 200, 50);

    /// <summary>Unity GUI Window ID (must be unique or will cause issues).</summary>
    public int WindowId = 100;

    public bool Pickup = false;
    public bool Controls = false;

    public void interactable(bool pickup)
    {
        this.Pickup = pickup;
        this.GUI_WindowOn = pickup;
    }

    // Start is called before the first frame update
    void Start()
    { 
        this.GuiPopup.width = 300;
        if (this.GuiPopup.x <= 0)
        {
            this.GuiPopup.x = Screen.width/2 - this.GuiPopup.width/2;
            this.GuiPopup.y = Screen.height / 2  + 50;
        }
        Controls = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.GUI_WindowOn = !this.GUI_WindowOn;
        }
    }

    public void OnGUI()
    {
        if (!this.GUI_WindowOn)
        {
            return;
        }
        this.GuiPopup = GUILayout.Window(this.WindowId, this.GuiPopup, this.Ui_PopUp, "Press E to Open/Close");
    }

    public void Ui_PopUp(int windowID)
    {

        if (this.Controls)
        {
            GUILayout.Box("Controls");
            GUILayout.Label("Use WASD to wallk around");
            GUILayout.Label("Use your mouse to look around");
        }

        if (this.Pickup)
        {
            GUILayout.Box("Pickup");
            GUILayout.Label("Items such as plants and some cabinets can be inteacted with by getting close to then and mouse clicking while the cross ahir is over them");
        }
        // if anything was clicked, the height of this window is likely changed. reduce it to be layouted again next frame
        if (GUI.changed)
        {
            this.GuiPopup.height = 100;
        }
    }
}

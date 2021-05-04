using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("the ui panel to let the user enter a name conect and play")]
    [SerializeField]
    private GameObject controlPanel;

    [Tooltip("the ui label to inform you that you are connecting")]
    [SerializeField]
    private GameObject progressionLabel;

    [Tooltip("The Maximum number of playera per room is 4. when the room is full, it cant be joined by new players, and so a new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    #endregion

    #region Private Fields
    // to help distingish the user game version
    string gameVersion = "1";
    bool isConnecting;
    #endregion

    #region MonoBehavior CallBacks
    // called in early initrilisation
    void Awake()
    {
        //allows the uses of photonNetwork.loadLevel so all clints can sync automaticaly
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        progressionLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    #endregion

    #region Public Methods
    public void Connect()
    {
        progressionLabel.SetActive(true);
        controlPanel.SetActive(false);
        // check to see if we are connected is we are join a random room
        // if not we try and connect to the server
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    #endregion

    #region MonoBehaviorPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {    
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("we load the 'Room for 1'");
            PhotonNetwork.LoadLevel("Room for 1");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
        progressionLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lobby_Controller : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [Tooltip("the maximum plqyers per room is 4")]
    [SerializeField]
    private byte MaxplayersPerRoom = 4;
    string RoomName = "1234";

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public GameObject NamePanel;
    public GameObject LobbyPanel;
    public GameObject RoomPanel;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }

    public void Connect()
    {        
        if (PhotonNetwork.IsConnected)
        {
            return;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.JoinLobby();
            NamePanel.SetActive(false);       
            LobbyPanel.SetActive(true);
        }
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomName, new RoomOptions { MaxPlayers = MaxplayersPerRoom });
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);       
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
        
    }
}

using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Lobby_Controller : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [Tooltip("the maximum players per room is 4")]
    [SerializeField]
    private byte MaxplayersPerRoom = 4;
   
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    #region gameobjects
    [SerializeField]
    public GameObject NamePanel;
    [SerializeField]
    public GameObject LobbyPanel;
    [SerializeField]
    public GameObject RoomPanel;
    [SerializeField]
    public GameObject StartButton;
    [SerializeField]
    public InputField RoomNameID;
    [SerializeField]
    public GameObject RoomNameHeaderID;
    [SerializeField]
    public InputField NameID;
    [SerializeField]
    public GameObject RoomList;
    [SerializeField]
    public GameObject PlayerList;
    [SerializeField]
    public GameObject RoomListPrefab;
    [SerializeField]
    public GameObject PlayerListPrefab;
    #endregion

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }

    public void JoinLobby()
    {
        PhotonNetwork.NickName = NameID.text;
        PhotonNetwork.JoinLobby();
        NamePanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }

    public void updateList()
    {
        for (int i = 0; i >= PhotonNetwork.PlayerList.Length; i++)
        {
            GameObject temp = Instantiate(PlayerListPrefab, PlayerList.transform);
            temp.GetComponentInChildren<Text>().text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomNameID.text, new RoomOptions { MaxPlayers = MaxplayersPerRoom });
        RoomNameHeaderID.GetComponent<Text>().text = RoomNameID.text;
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

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }
    public void leaveLobby()
    {
        PhotonNetwork.LeaveLobby();
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
    }

    public void CreateRoomJoin(string RoomInfo)
    {
        string[] RoomInfoSplit = RoomInfo.Split(' ');
        string roomname = RoomInfoSplit[0] + " " + RoomInfoSplit[1];
        string players = RoomInfoSplit[4] + " " + RoomInfoSplit[3];
        GameObject temp = Instantiate(RoomListPrefab, RoomList.transform);
        temp.GetComponentInChildren<Text>().text = roomname + " " + players;
    }

    public void RemoveAllRooms()
    {
        while (RoomList.transform.childCount > 0)
        {
            GameObject.Destroy(RoomList.transform.GetChild(0));
        }
    }

    #region overrides
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby() called by PUN. Now this client is in the lobby.");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        { 
            RoomNameHeaderID.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;
            RoomPanel.SetActive(true);
            StartButton.SetActive(true);           
        }
        else
        {
            RoomNameHeaderID.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;
            RoomPanel.SetActive(true);
            StartButton.SetActive(false);            
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("faild to join room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("faild to create room");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
        if (roomList.Count > 0)
        {
            RemoveAllRooms();
            for (int i = 0; i <= roomList.Count; i++)
            {
                Debug.Log(roomList[0]);
                CreateRoomJoin(roomList[i].ToString());
            }
        }
    }
    #endregion
}

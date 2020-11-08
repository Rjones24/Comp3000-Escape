using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Reflection;

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
    public Transform RoomList;
    [SerializeField]
    public Transform PlayerLists;
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
        for (int i = 0; i == PhotonNetwork.PlayerList.Length-1; i++)
        {
            string nickname = PhotonNetwork.PlayerList[i].NickName;
            Debug.Log(nickname);
            GameObject temp = Instantiate(PlayerListPrefab, PlayerLists.GetChild(i));            
            temp.GetComponentInChildren<Text>().text = nickname;
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
            if ((info.RemovedFromList)||(info.PlayerCount ==0 ))
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
        RemoveAllPlayerNames();
        updateList();
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

    public void CreateRoomJoin(string RoomInfo, int i)
    {
        string[] RoomInfoSplit = RoomInfo.Split(' ');
        string roomname = RoomInfoSplit[0] + " " + RoomInfoSplit[1];
        string players = RoomInfoSplit[4] + " " + RoomInfoSplit[3];
        GameObject temp = Instantiate(RoomListPrefab, RoomList.GetChild(i));
        temp.GetComponentInChildren<Text>().text = roomname + " " + players;
    }

    public void RemoveAllPlayerNames()
    {
        for (int i = 0; i <= PlayerLists.childCount; i++)
        {
            Transform temp = PlayerLists.GetChild(i);
            if (temp.childCount == 0)
            {
                return;
            }
            else
            {
                Destroy(temp.GetChild(0).gameObject);
            }
        }
    }

    public void RemoveAllRooms()
    {
        for (int i = RoomList.childCount - 1; i >= 0; i--) 
        {
            Transform temp = RoomList.GetChild(i);
            if(temp.childCount== 0)
            {
                return;
            }
            else
            {
                Destroy(temp.GetChild(0).gameObject);
            } 
        }
    }

    public void refreshPlayerList()
    {
        for (int i = 0; i == PhotonNetwork.PlayerList.Length - 1; i++)
        {
            string nickname = PhotonNetwork.PlayerList[i].NickName;
            Debug.Log(nickname);
        }
        RemoveAllPlayerNames();    
        updateList();
    }

#region overrides
public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby() called by PUN. Now this client is in the lobby.");
    }

    public override void OnJoinedRoom()
    {
        RemoveAllPlayerNames();
        updateList();
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
            for (int i = 0; i <= roomList.Count-1; i++)
            {
                Debug.Log(roomList[i]);
                CreateRoomJoin(roomList[i].ToString(), i);
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    #endregion
}

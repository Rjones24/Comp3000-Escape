using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Reflection;

public class Lobby_Controller : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [Tooltip("the maximum players per room is 2")]
    [SerializeField]
    private byte MaxplayersPerRoom = 2;
   
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    #region gameobjects
    [SerializeField]
    public GameObject NamePanel;
    [SerializeField]
    public GameObject LobbyPanel;
    [SerializeField]
    public GameObject Connecting;
    [SerializeField]
    public GameObject JoinButton;
    [SerializeField]
    public InputField RoomNameID;
    [SerializeField]
    public GameObject RoomNameHeaderID;
    [SerializeField]
    public InputField NameID;
    [SerializeField]
    public Transform RoomList;
    [SerializeField]
    public GameObject RoomListPrefab;
    #endregion

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }

    public void JoinLobby()
    {
        PhotonNetwork.NickName = NameID.text;
        PhotonNetwork.JoinLobby();
        NamePanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomNameID.text, new RoomOptions { MaxPlayers = MaxplayersPerRoom });
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if ((info.RemovedFromList)||(info.PlayerCount == 0 ))
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }

    public void leaveLobby()
    {
        PhotonNetwork.LeaveLobby();
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }

    public void CreateRoomJoin(string RoomInfo, int i)
    {
        string[] RoomInfoSplit = RoomInfo.Split(' ');
        string roomname = RoomInfoSplit[0] + " " + RoomInfoSplit[1];
        string players = RoomInfoSplit[4] + " " + RoomInfoSplit[3];
        GameObject temp = Instantiate(RoomListPrefab, RoomList.GetChild(i));
        temp.GetComponentInChildren<Text>().text = roomname + " " + players;
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

#region overrides
public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby() called by PUN. Now this client is in the lobby.");
    }
    
    public override void OnCreatedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
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
        Connecting.SetActive(false);
        JoinButton.SetActive(true);
    }
    #endregion
}

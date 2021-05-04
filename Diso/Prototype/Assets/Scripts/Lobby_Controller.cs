using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Lobby_Controller : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";

    [Tooltip("the maximum players per room is 2")]
    [SerializeField]
    private byte MaxplayersPerRoom = 2;

    [Header("Room List Panel")]
    [SerializeField]
    public GameObject LobbyPanel;

    public GameObject RoomListContent;
    public GameObject RoomListEntryPrefab;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;

    #region gameobjects
    [SerializeField]
    public GameObject NamePanel;
    [SerializeField]
    public GameObject Connecting;
    [SerializeField]
    public GameObject JoinButton;
    public GameObject Invalid;
    [SerializeField]
    public InputField RoomNameID;
    [SerializeField]
    public GameObject RoomNameHeaderID;
    [SerializeField]
    public InputField NameID;
    #endregion

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string, GameObject>();
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }

    public void JoinLobby()
    {
           string playerName = NameID.text;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.JoinLobby();
            NamePanel.SetActive(false);
            LobbyPanel.SetActive(true);
        }
        else
        {
            Invalid.SetActive(true);
        }
      
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(RoomNameID.text, new RoomOptions { MaxPlayers = MaxplayersPerRoom });
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // Remove room from cached room list if it got closed, became invisible or was marked as removed
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }
                continue;
            }
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }

    private void ClearRoomListView()
    {
        foreach (GameObject entry in roomListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        roomListEntries.Clear();
    }

    public void leaveLobby()
    {
        cachedRoomList.Clear();
        ClearRoomListView();
        NamePanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }

#region overrides
public override void OnJoinedLobby()
    {
        cachedRoomList.Clear();
        ClearRoomListView();
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
        ClearRoomListView();
        UpdateCachedRoomList(roomList);
        UpdateRoomListView();
    }

    private void UpdateRoomListView()
    {
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            GameObject entry = Instantiate(RoomListEntryPrefab);
            entry.transform.SetParent(RoomListContent.transform);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<RoomListEntry>().Initialize(info.Name, (byte)info.PlayerCount, info.MaxPlayers);

            roomListEntries.Add(info.Name, entry);
        }
    }

    public override void OnConnectedToMaster()
    {
        Connecting.SetActive(false);
        JoinButton.SetActive(true);
    }
    #endregion
}

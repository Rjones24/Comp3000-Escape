using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class Room_Controller : MonoBehaviourPunCallbacks
{

    public const string PLAYER_READY = "IsPlayerReady";

    [SerializeField]
    public GameObject LobbyPanel;
    [SerializeField]
    public GameObject StartPanel;

    [SerializeField]
    public GameObject RoomPanel;
    public GameObject PlayerListPrefab;
    [SerializeField]
    public GameObject StartButton;
    [SerializeField]
    public Transform PlayerLists;

    [SerializeField]
    public GameObject RoomNameHeaderID;

    private Dictionary<string, GameObject> playerListEntries;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(PLAYER_READY, out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public override void OnJoinedRoom()
    {
        LobbyPanel.SetActive(false); 
        RoomPanel.SetActive(true);

        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<string, GameObject>();
        }

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entry = Instantiate(PlayerListPrefab);
            entry.transform.SetParent(PlayerLists);
            entry.transform.localScale = Vector3.one;
            entry.GetComponent<PlayerListEntity>().Initialize(p.ActorNumber, p.NickName);

            object isPlayerReady;
            if (p.CustomProperties.TryGetValue(PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<PlayerListEntity>().SetPlayerReady((bool)isPlayerReady);
            }

            playerListEntries.Add(p.NickName, entry);
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartButton.gameObject.SetActive(CheckPlayersReady());
        }

        RoomNameHeaderID.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnLeftRoom()
    {
        foreach (GameObject entry in playerListEntries.Values)
        {
            Destroy(entry.gameObject);
        }

        playerListEntries.Clear();
        playerListEntries = null;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject entry = Instantiate(PlayerListPrefab);
        entry.transform.SetParent(PlayerLists.transform);
        entry.transform.localScale = Vector3.one;
        entry.GetComponent<PlayerListEntity>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

        playerListEntries.Add(newPlayer.NickName, entry);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Destroy(playerListEntries[otherPlayer.NickName].gameObject);
        playerListEntries.Remove(otherPlayer.NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                StartButton.gameObject.SetActive(CheckPlayersReady());
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (playerListEntries == null)
        {
            playerListEntries = new Dictionary<string, GameObject>();
        }

        GameObject entry;
        if (playerListEntries.TryGetValue(targetPlayer.NickName, out entry))
        {
            object isPlayerReady;
            if (changedProps.TryGetValue(PLAYER_READY, out isPlayerReady))
            {
                entry.GetComponent<PlayerListEntity>().SetPlayerReady((bool)isPlayerReady);
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount ==2)
        {
          StartButton.gameObject.SetActive(CheckPlayersReady());
        }
      
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("faild to join room");
    }

    public void leaveRoom()
    {
        RoomPanel.SetActive(false);
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        StartPanel.SetActive(true);
    }

    public void LocalPlayerPropertiesUpdated()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            StartButton.gameObject.SetActive(CheckPlayersReady());
        }
    }

    public void startGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        SceneManager.LoadScene(1);
    }
}

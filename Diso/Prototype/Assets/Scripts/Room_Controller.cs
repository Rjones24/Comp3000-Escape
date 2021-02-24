using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Room_Controller : MonoBehaviourPunCallbacks
{

    [SerializeField]
    public GameObject LobbyPanel;
    [SerializeField]
    public GameObject RoomPanel;
    [SerializeField]
    public GameObject StartButton;
    [SerializeField]
    public Transform PlayerLists;
    [SerializeField]
    public GameObject PlayerListPrefab;
    [SerializeField]
    public GameObject RoomNameHeaderID;

    public override void OnJoinedRoom()
    {
        LobbyPanel.SetActive(false);
        RoomNameHeaderID.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.Name;
        RoomPanel.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
        RemoveAllPlayerNames();
        updateList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RemoveAllPlayerNames();
        updateList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveAllPlayerNames();
        updateList();
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("faild to join room");
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

    IEnumerator RejoinJLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void leaveRoom()
    {
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(RejoinJLobby()); 
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void updateList()
    {
        for(int i=0; i<= PhotonNetwork.PlayerList.Length - 1; i++)
        {
            string nickname = PhotonNetwork.PlayerList[i].NickName;
            GameObject temp = Instantiate(PlayerListPrefab, PlayerLists.GetChild(i));
            temp.GetComponentInChildren<Text>().text = nickname;
        }
    }
}

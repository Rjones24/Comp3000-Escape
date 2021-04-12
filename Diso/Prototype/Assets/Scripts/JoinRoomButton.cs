using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Collections;


public class JoinRoomButton : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private Transform LobbyPanel;
    string roomname;

    public void JoinRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        LobbyPanel = this.transform.parent;
        LobbyPanel = LobbyPanel.transform.parent;
        LobbyPanel = LobbyPanel.transform.parent;
        string roomInfo = this.GetComponentInChildren<Text>().text;
        string[] RoomInfoSplit = roomInfo.Split(' ');
        roomname = RoomInfoSplit[1];
        roomname = roomname.Trim('\'');
        PhotonNetwork.JoinRoom(roomname);
    }    
}

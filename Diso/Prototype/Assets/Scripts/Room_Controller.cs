using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Room_Controller : MonoBehaviour
{
    [SerializeField]
    public Transform LobbyPanel;

    public void JoinRoom()
    {
        LobbyPanel = this.transform.parent;
        LobbyPanel = LobbyPanel.transform.parent;
        LobbyPanel = LobbyPanel.transform.parent;
        string roomInfo = this.GetComponentInChildren<Text>().text;
        string[] RoomInfoSplit = roomInfo.Split(' ');
        string roomname = RoomInfoSplit[1];
        roomname = roomname.Trim('\'');
        PhotonNetwork.JoinRoom(roomname);
        LobbyPanel.gameObject.SetActive(false);
    }

}

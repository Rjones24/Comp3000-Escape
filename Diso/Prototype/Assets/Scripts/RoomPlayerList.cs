using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoomPlayerList : MonoBehaviour
{

    [SerializeField]
    public GameObject PlayerList;
    [SerializeField]
    public GameObject PlayerListPrefab;

    public void updateList()
    {
        for(int i=0; i>= PhotonNetwork.PlayerList.Length; i++)
        {
            GameObject temp = Instantiate(PlayerListPrefab, PlayerList.transform);
            temp.GetComponentInChildren<Text>().text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

}

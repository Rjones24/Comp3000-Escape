using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using ExitGames.Client.Photon;

public class PlayerListEntity : MonoBehaviourPunCallbacks
{
    public Text PlayerName;
    public Button ReadyUp;
    public Image PlayerReadyImage;

    private bool isPlayerReady;
    private int ownerId;

    public void Start()
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerId)
        {
            ReadyUp.gameObject.SetActive(false);
        }
        else
        {
            ReadyUp.onClick.AddListener(() =>
            {
                isPlayerReady = !isPlayerReady;
                SetPlayerReady(isPlayerReady);

                Hashtable props = new Hashtable() { { Room_Controller.PLAYER_READY, isPlayerReady } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);


            if (PhotonNetwork.IsMasterClient)
            {
                    FindObjectOfType<Room_Controller>().LocalPlayerPropertiesUpdated();
                }
            });
        }
    }

    public void Initialize(int playerId, string name) {
        ownerId = playerId;
        PlayerName.text = name;

    }

    public void SetPlayerReady(bool playerReady)
    {
        ReadyUp.GetComponentInChildren<Text>().text = playerReady ? "Ready!" : "Ready?";
        PlayerReadyImage.enabled = playerReady;
    }
}

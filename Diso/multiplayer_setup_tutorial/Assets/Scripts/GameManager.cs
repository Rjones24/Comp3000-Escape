using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if(playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing<a><Color> playerPrefab reference. please set up in game manger.", this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInatance == null)
            {
                Debug.LogFormat("we are instantiating LocalPlayer from{0}", Application.loadedLevelName);
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
            }
        }
    }

    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnterdRoom() {0}", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnterdRoom() IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom()  {0}", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogErrorFormat("OnPlayerLeftRoom() IsMasterClient {0}");
            LoadArena();
        }
    }
    #endregion

    #region public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region Public Fields
    [Tooltip("the prefab to use for representing the player")]
    public GameObject playerPrefab;
    #endregion

    #region Private Methods
    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : trying to load a level but we are not the master");
        }
        Debug.LogFormat("Photon: Loading level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
}

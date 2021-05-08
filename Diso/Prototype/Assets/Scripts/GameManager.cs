using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("the prefab to use for representing the player")]
    public GameObject playerPrefab;
    public List<Vector3> Spawn;
   
    public static bool win = false;

    void Start()
    {
        Cursor.visible.Equals(false);
        Spawn.Add(new Vector3(-2.5f, 1.5f, 0f));
        Spawn.Add(new Vector3(-45f, 1.5f, 0f)); 

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing<a><Color> playerPrefab reference. please set up in game manger.", this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInatance == null)
            {
                for (int i = 0; i <= PhotonNetwork.CountOfPlayersInRooms; i++)
                {
                    if (PhotonNetwork.PlayerList[i].NickName == PhotonNetwork.LocalPlayer.NickName)
                    {
                        Debug.LogFormat("we are instantiating LocalPlayer", SceneManagerHelper.ActiveSceneName);
                        PhotonNetwork.Instantiate(this.playerPrefab.name, Spawn[i], Quaternion.identity, 0);
                    }
                }
            }
            else
            {
                Debug.LogFormat("Ignoring scene load ", SceneManagerHelper.ActiveSceneName);
            }
        }
    }

}


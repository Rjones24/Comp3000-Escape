using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Tooltip("the prefab to use for representing the player")]
    public GameObject playerPrefab;
    public List<Vector3> Spawn;
    

    public static bool win = false;

    static public GameManager Instance;
    void Start()
    {
        Spawn.Add(new Vector3(-2.5f, 1.5f, 0f));
        Spawn.Add(new Vector3(-45f, 1.5f, 0f)); 
        Instance = this;

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
                        Debug.LogFormat("we are instantiating LocalPlayer from{0}", SceneManagerHelper.ActiveSceneName);
                        PhotonNetwork.Instantiate(this.playerPrefab.name, Spawn[i], Quaternion.identity, 0);

                    }
                }
            }
            else
            {
                Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);

            }
        }
    }

    private void Update()
    {
        if (win)
        {
            winstate();
            win = false;
        }
    }


    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : trying to load a level but we are not the master");

        }
        Debug.LogFormat("Photon: Loading level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void winstate()
    {
        SceneManager.LoadScene("Win", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.None;
    }
}


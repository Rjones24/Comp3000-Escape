﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    [Tooltip("the local player instance")]
    public static GameObject LocalPlayerInatance;

    [Tooltip("The Player's name GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;

    [SerializeField]
    public GameObject hands;

    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }


    void CalledOnLevelWasLoaded(int level)
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
        
    }

    public override void OnDisable()
    {
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInatance = this.gameObject;

            var ItemPickup = GameObject.FindGameObjectsWithTag("Pickups");
            for (int i = 0; i < ItemPickup.Length; i++)
            {
                ItemPickup[i].SendMessage("PLayersPos", this.hands, SendMessageOptions.RequireReceiver);
            }
            var CardPickup = GameObject.FindGameObjectsWithTag("Card");
            for (int j = 0; j < CardPickup.Length; j++)
            {
                CardPickup[j].SendMessage("PLayersPos", this.hands, SendMessageOptions.RequireReceiver);
            }
            var KeyPickup = GameObject.FindGameObjectsWithTag("key");
            for (int j = 0; j < KeyPickup.Length; j++)
            {
                KeyPickup[j].SendMessage("PLayersPos", this.hands, SendMessageOptions.RequireReceiver);
            }

        }
        DontDestroyOnLoad(this.gameObject);
    }
}

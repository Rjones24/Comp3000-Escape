using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    [Tooltip("the local player instance")]
    public static GameObject LocalPlayerInatance;

    // Start is called before the first frame update
    void Start()
    {
        CameraWork _camerawork = this.gameObject.GetComponent<CameraWork>();

        if(_camerawork != null)
        {
            if (photonView.IsMine)
            {
                _camerawork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing<a><Color< CameraWork Component on PlayerPrefab.", this);
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }

   void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);    
    }

    void CalledOnLevelWasLoaded(int level)
    {
        if(!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInatance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

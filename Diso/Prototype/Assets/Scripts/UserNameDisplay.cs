using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNameDisplay : MonoBehaviourPunCallbacks
{

    [Tooltip("Text to display Player's Name")]
    [SerializeField]
    private TextMesh playerUiPrefab;

    Transform targetTransform;
    Vector3 targetPosition;
    Quaternion targetRotation;

    private PlayerManager target;

    public void SetTarget(PlayerManager _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        target = _target;

        targetTransform = this.target.GetComponent<Transform>();

        if (playerUiPrefab != null)
        {
            playerUiPrefab.text = target.photonView.Owner.NickName;
        }
    }

    void LateUpdate()
    {
        // #Critical
        // Follow the Target GameObject on screen.
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += 5.0f;
            this.transform.position = targetPosition;

            targetRotation = targetTransform.rotation;
            this.transform.rotation = targetRotation;
        }
    }

    void Awake()
    {
            this.transform.SetParent(GameObject.Find("PhotonMono").GetComponent<Transform>(), false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}

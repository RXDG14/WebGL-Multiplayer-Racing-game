using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIssuePUN : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
        //if(PhotonVoiceView)
    }
}

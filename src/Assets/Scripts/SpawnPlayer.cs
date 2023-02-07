using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    private PhotonVoiceView recorder;

    //public GoToLobby gotolobby;

    public GameObject playerPrefab_1;
    public GameObject playerPrefab_2;
    //public GameObject playerPrefab_3;
    //public GameObject playerPrefab_4;

    private void Start()
    {
        //SpawnPlayers(1);
        int j = PlayerPrefs.GetInt("i_value");
        if(j == 1)
        {
            SpawnPlayers(1);
        }
        if (j == 2)
        {
            SpawnPlayers(2);
        }
    }

    void SpawnPlayers(int playerNumber)
    {
        int spawnPointX = Random.Range(-45, 45);
        int spawnPointZ = Random.Range(-45, 45);
        Vector3 spawnPosition = new Vector3(0,5,0);// (spawnPointX, 3, spawnPointZ);
        if (playerNumber == 1)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.Instantiate(playerPrefab_1.name, new Vector3(-5, 0, 125), Quaternion.identity);
                PhotonVoiceView photonVoiceView = playerPrefab_1.GetComponent<PhotonVoiceView>();
                if (photonVoiceView != null)
                {
                    this.recorder = photonVoiceView;
                }
            }
            else
            {
                PhotonNetwork.Instantiate(playerPrefab_1.name, new Vector3(2.5f, 0, 125), Quaternion.identity);
                PhotonVoiceView photonVoiceView = playerPrefab_1.GetComponent<PhotonVoiceView>();
                if (photonVoiceView != null)
                {
                    this.recorder = photonVoiceView;
                }
            }
        }
        if (playerNumber == 2)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.Instantiate(playerPrefab_2.name, new Vector3(-5, 0, 125), Quaternion.identity);
                PhotonVoiceView photonVoiceView = playerPrefab_2.GetComponent<PhotonVoiceView>();
                if (photonVoiceView != null)
                {
                    this.recorder = photonVoiceView;
                }
            }
            else
            {
                PhotonNetwork.Instantiate(playerPrefab_2.name, new Vector3(2.5f, 0, 125), Quaternion.identity);
                PhotonVoiceView photonVoiceView = playerPrefab_2.GetComponent<PhotonVoiceView>();
                if (photonVoiceView != null)
                {
                    this.recorder = photonVoiceView;
                }
            }
        }
        /*if (playerNumber == 3)
        {
            PhotonNetwork.Instantiate(playerPrefab_3.name, spawnPosition, Quaternion.identity);
            PhotonVoiceView photonVoiceView = playerPrefab_3.GetComponent<PhotonVoiceView>();
            if (photonVoiceView != null)
            {
                this.recorder = photonVoiceView;
            }
        }
        if (playerNumber == 4)
        {
            PhotonNetwork.Instantiate(playerPrefab_4.name, spawnPosition, Quaternion.identity);
            PhotonVoiceView photonVoiceView = playerPrefab_4.GetComponent<PhotonVoiceView>();
            if (photonVoiceView != null)
            {
                this.recorder = photonVoiceView;
            }
        }*/
    }
}

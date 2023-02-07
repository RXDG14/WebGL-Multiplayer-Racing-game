using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviourPunCallbacks
{
    public PhotonView view;
    public GameObject pauseMenu;
    public TMP_Text countdownText;
    public GameObject startRaceButton;
    public GameObject enableCarInputs;
    public GameObject countdownBackground;

    int i = 1;
    int countdownTime = 5;
    bool raceStarted = false;

    void Start()
    {
        InvokeRepeating("CheckPlayers",2f,2f);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }    
    }

    public void ResumePressed()
    {
        pauseMenu.SetActive(false);
    }

    public void MenuPressed()
    {
        PhotonNetwork.LoadLevel(2);
        //PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom();
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    void CheckPlayers()
    {
        if (raceStarted == false)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                raceStarted = true;
                view.RPC("EnableStartRaceButton", RpcTarget.MasterClient);
            }
            else
            {
                countdownText.text = "Please wait for players to join";
            }
        }
        else
        {
            return;
        }
    }

    public void StartRacePressed()
    {
        view.RPC("StartTimer", RpcTarget.AllViaServer);
        startRaceButton.SetActive(false);
    }

    IEnumerator CountDown()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        if(countdownTime == 0) 
        {
            enableCarInputs.transform.position = Vector3.zero;
            countdownBackground.SetActive(false);
        }
    }

    [PunRPC]
    void EnableStartRaceButton()
    {
        startRaceButton.SetActive(true);
    }

    [PunRPC]
    void StartTimer()
    {
        StartCoroutine(CountDown());
    }

    [PunRPC]
    void EndRace()
    {

    }

    [PunRPC]
    void RacaePosition(string playerName)
    {
        
    }
}

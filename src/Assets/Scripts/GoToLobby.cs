using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLobby : MonoBehaviour
{
    public static int i = 1;
    public TMP_Text playerName;

    public GameObject player1;
    public GameObject player2;

    public void Start()
    {
        i = 1;
        EnableDisablePlayers(i);
        playerName.text = "Welcome " + PhotonNetwork.NickName;
    }

    public void OnPreviousPressed()
    {
        i--;
        if (i < 1)
        {
            i = 2;
        }
        EnableDisablePlayers(i);
    }

    public void OnNextPressed()
    {
        i++;
        if (i > 2)
        {
            i = 1;
        }
        EnableDisablePlayers(i);
    }

    void EnableDisablePlayers(int i)
    {
        if (i == 1)
        {
            player1.SetActive(true);
            player2.SetActive(false);
        }
        if (i == 2)
        {
            player1.SetActive(false);
            player2.SetActive(true);
        }
    }

    public void OnPlayPressed()
    {
        PlayerPrefs.SetInt("i_value", i);
        SceneManager.LoadScene(3);
    }
}

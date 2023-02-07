using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviourPunCallbacks
{
    PhotonView view;
    List<string> messages = new List<string>();

    int maxMessages = 5;
    int i = 1;
    float buildDelay = 0f;

    public GameObject leaderBoard;
    public GameObject stop;
    public TextMeshProUGUI chatContent;

    public int pos = 1;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            chatContent.maxVisibleLines = maxMessages;
            if (messages.Count > maxMessages)
            {
                messages.RemoveAt(0);
            }
            if (buildDelay < Time.time)
            {
                BuildChatContents();
                buildDelay = Time.time + 0.25f;
            }
        }
        else if (messages.Count > 0)
        {
            messages.Clear();
            chatContent.text = "";
        }
    }

    [PunRPC]
    void RacePosition(string playerName)
    {
        chatContent.enabled = true;
        messages.Add("#" + pos + " : " + playerName);
        pos++;
    }

    [PunRPC]
    void EnableLeaderboardBackground()
    {
        leaderBoard.SetActive(true);
        stop.SetActive(true);
    }

    void BuildChatContents()
    {
        string NewContents = "";
        foreach (string s in messages)
        {
            NewContents += s + "\n";
        }
        chatContent.text = NewContents;
    }

}

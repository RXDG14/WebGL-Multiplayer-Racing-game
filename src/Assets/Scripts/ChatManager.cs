using Photon.Pun;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    PhotonView view;
    //PhotonVoiceView voiceView;
    List<string> messages = new List<string>();
    
    int maxMessages = 5;
    int i = 1;
    float buildDelay = 0f;

    //chat...............................................
    public GameObject hideButton;
    public GameObject chatWindow;
    public TMP_InputField chatInput;
    public TextMeshProUGUI chatContent;
    public GameObject chatContentBG;
    

    private void Start()
    {
        view = GetComponent<PhotonView>();
        //voiceView = GetComponent<PhotonVoiceView>();
    }

    private void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            chatContent.maxVisibleLines = maxMessages;
            if(messages.Count > maxMessages)
            {
                messages.RemoveAt(0);
            }
            if(buildDelay < Time.time)
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
    void RPC_AddNewMessage(string msg)
    {
        chatContent.enabled = true;
        chatContentBG.SetActive(true);
        hideButton.SetActive(true);
        messages.Add(msg);
    }

    public void SendChat(string msg)
    {
        string NewMessage = PhotonNetwork.NickName + " : " + msg;
        view.RPC("RPC_AddNewMessage", RpcTarget.All, NewMessage);
    }

    public void SubmitChat()
    {
        string blankCheck = chatInput.text;
        blankCheck = Regex.Replace(blankCheck, @"\s", "");
        if(blankCheck == "")
        {
            chatInput.ActivateInputField();
            chatInput.text = "";
            return;
        }

        SendChat(chatInput.text);
        chatInput.ActivateInputField();
        chatInput.text = "";
    }

    void BuildChatContents()
    {
        string NewContents = "";
        foreach(string s in messages)
        {
            NewContents += s + "\n";
        }
        chatContent.text = NewContents;
    }

    public void OnChatButtonPressed()
    {
        i += 1;
        if (i % 2 == 0)
        {
            chatWindow.SetActive(true);
            chatContent.enabled = true;
            chatContentBG.SetActive(true);
            hideButton.SetActive(true);
        }
        else
        {
            chatWindow.SetActive(false);
        }
    }

    public void OnHideButtonPressed()
    {
        i = 1;
        chatContent.enabled = false;
        chatContentBG.SetActive(false);
        chatWindow.SetActive(false);
        hideButton.SetActive(false);
    }

}

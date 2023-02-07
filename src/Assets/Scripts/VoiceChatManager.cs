using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class VoiceChatManager : MonoBehaviour
{
    public TMP_Text muteUnmuteText;

    private PhotonVoiceView recorder;
    private PunVoiceClient punVoiceClient;
    
    int i = 1;
    private void Awake()
    {
        punVoiceClient = PunVoiceClient.Instance;
    }
    public void OnThisButtonPressed()
    {
        i += 1;
        if (i % 2 == 0)
        {
            punVoiceClient.PrimaryRecorder.TransmitEnabled = true;
            muteUnmuteText.text = "Mute";
        }
        else
        {
            punVoiceClient.PrimaryRecorder.TransmitEnabled = false;
            muteUnmuteText.text = "Unmute";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            i += 1;
            if (i % 2 == 0)
            {
                punVoiceClient.PrimaryRecorder.TransmitEnabled = true;
                muteUnmuteText.text = "Mute";
            }
            else
            {
                punVoiceClient.PrimaryRecorder.TransmitEnabled = false;
                muteUnmuteText.text = "Unmute";
            }
        }
    }
}

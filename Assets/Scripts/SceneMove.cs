using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class SceneMove : MonoBehaviourPunCallbacks
{
    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectAndLoadMainMenu();
    }

    public void DisconnectAndLoadMainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("SampleScene");
    }
}
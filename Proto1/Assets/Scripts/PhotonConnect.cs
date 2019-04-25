using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhotonConnect : MonoBehaviour
{
    public string versionName = "0.1";

    public void connectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.ConnectToBestCloudServer();
        //PhotonNetwork.ConnectToRegion("EU");
        Debug.Log("Connecting to photon...");
    }

    private void OnConnectedToServer()
    {
        //dit werkt niet
        Debug.Log("Connected to photon");
    }

    //private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    //{
        
    //}


}

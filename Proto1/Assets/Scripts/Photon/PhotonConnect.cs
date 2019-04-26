using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    //Menustuff
    [SerializeField]
    InputField inputFieldConnect, inputFieldCreate;

    //PhotonStuff
    public string versionName = "0.1";


    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        connectToPhoton();
    }

   

    private void connectToPhoton()
    {
        PhotonNetwork.AddCallbackTarget(PhotonNetwork.ConnectUsingSettings());
        //PhotonNetwork.ConnectToBestCloudServer();
        //PhotonNetwork.ConnectToRegion("EU");
        Debug.Log("Connecting to photon...");
    }
    //Check methods
    // ik kan geen callbacks want ik ben een loser
    public void CheckConnection()
    {
        Debug.Log(PhotonNetwork.IsConnected);
    }
    public void CheckRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
        //Debug.Log(PhotonNetwork.CurrentLobby);
    }

    private void OnConnectedToServer()
    {
        //dit werkt niet
        Debug.Log("Connected to photon");
    }
    //Room methodes
    public void onClickCreateRoom()
    {
        
        PhotonNetwork.CreateRoom(inputFieldCreate.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void onClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(inputFieldConnect.text);
    }

    //Start game methode

    public void onClickStartGame()
    {
        PhotonNetwork.LoadLevel(17);
    }

    //private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
    //{

    //}


}

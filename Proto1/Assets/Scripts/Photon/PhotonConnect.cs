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
    [SerializeField]
    Text debugText;

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

   

    public void connectToPhoton()
    {
        PhotonNetwork.AddCallbackTarget(PhotonNetwork.ConnectUsingSettings());
        PhotonNetwork.ConnectToBestCloudServer();
        Debug.Log("Connecting to photon...");
    }
    //Check methods
    // ik kan geen callbacks want ik ben een loser
    public void CheckConnection()
    {
        Debug.Log(PhotonNetwork.IsConnected);
        debugText.text = PhotonNetwork.IsConnected.ToString();
        debugText.text = debugText.text + " location: " +PhotonNetwork.CloudRegion;
    }
    public void CheckRoom()
    {
        Debug.Log(PhotonNetwork.CountOfPlayers);
        Debug.Log(PhotonNetwork.CurrentRoom);
        debugText.text = PhotonNetwork.CurrentRoom.ToString();
    }

    private void OnConnectedToServer()
    {
        //dit werkt niet
        Debug.Log("Connected to photon");
    }
    //Room methodes
    public void onClickJoinOrCreateRandomRoom()
    {
        //super vies maar is voor testen
        if(PhotonNetwork.CountOfRooms==0)
            PhotonNetwork.CreateRoom("randomRoom", new RoomOptions() { MaxPlayers = 2} ,null);
        else
            PhotonNetwork.JoinRandomRoom();
    }
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
